using System.IO;
using UnityEngine;

public class SheetStorage : MonoBehaviour
{
    /*
     * 저장
        1) 노트 오브젝트 읽어서 y좌표 기반으로 시간 계산
        BarPerSec / 16 * 노트y좌표 = 저장될 시간

        롱노트의 경우
        Head y좌표 = NoteLong의 y좌표
        Tail y좌표 = NoteLong.y + tail.y가 최종좌표
     */


    void Start()
    {

    }

    public void Save()
    {
        Sheet sheet = GameManager.Instance.sheets[GameManager.Instance.title];

        string notes = string.Empty;
        float baseTime = sheet.BarPerSec / 16;
        foreach (NoteObject note in NoteGenerator.Instance.toReleaseList)
        {
            if (!note.gameObject.activeSelf) // 비활성화되어있다면 삭제된 노트이므로 무시
                continue;

            float line = note.transform.position.x;
            int findLine = 0;
            if (line < -1f && line > -2f)
            {
                findLine = 0;
            }
            else if (line < 0f && line > -1f)
            {
                findLine = 1;
            }
            else if (line < 1f && line > 0f)
            {
                findLine = 2;
            }
            else if (line < 2f && line > 1f)
            {
                findLine = 3;
            }

            if (note is NoteShort)
            {
                NoteShort noteShort = note as NoteShort;
                int noteTime = (int)(noteShort.transform.position.y * baseTime * 1000);

                notes += $"{noteTime}, {(int)NoteType.Short}, {findLine + 1}\n";
            }
            else if (note is NoteLong)
            {
                NoteLong noteLong = note as NoteLong;
                int headTime = (int)(noteLong.transform.position.y * baseTime * 1000);
                int tailTime = (int)((noteLong.transform.position.y + noteLong.tail.transform.position.y - noteLong.transform.position.y) * baseTime * 1000);
                notes += $"{headTime}, {(int)NoteType.Long}, {findLine + 1}, {tailTime}\n";
            }
        }

        string writer = $"[Description]\n" +
            $"Title: {sheet.title}\n" +
            $"Artist: {sheet.artist}\n\n" +
            $"[Audio]\n" +
            $"BPM: {sheet.bpm}\n" +
            $"Offset: {sheet.offset}\n" +
            $"Signature: {sheet.signature[0]}/{sheet.signature[1]}\n\n" +
            $"[Note]\n" +
            $"{notes}";

        writer.TrimEnd('\r', '\n');

        string pathSheet = $"{Application.dataPath}/Sheet/{sheet.title}/{sheet.title}.sheet";
        if (File.Exists(pathSheet))
        {
            try
            {
                File.Delete(pathSheet);
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);
                return;
            }
        }

        if (!File.Exists(pathSheet))
        {
            using (FileStream fs = File.Create(pathSheet))
            {

            }
        }
        else
        {
            Debug.LogError($"{sheet.title}.sheet가 이미 존재합니다 !");
            return;
        }

        using (StreamWriter sw = new StreamWriter(pathSheet))
        {
            sw.Write(writer);
        }
    }
}
