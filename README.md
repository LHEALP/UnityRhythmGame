# UnityRhythmGame2
유니티엔진 리듬게임2</br>
</br>
이 프로젝트는 유니티 2021.3.1f1에서 생성되었습니다.</br>
</br>

Game</br>
![GIF 2022-08-05 오전 12-28-40](https://user-images.githubusercontent.com/57874136/182889438-4359bff0-f735-4736-8f56-fc495e2ecae2.gif)
</br>
</br>
Editor</br>
![GIF 2022-08-05 오전 12-32-01](https://user-images.githubusercontent.com/57874136/182889528-422b9e21-5191-495f-998b-537cfbb44dbf.gif)
</br>
</br>

## 2.0 주요 변경점
* 에디터 추가</br>
* 롱노트 추가</br>
</br>

## OST - Smilgate RPG
* 영혼을 데우는 스프(Consolation)</br>
* 화려한 서커스(Splendid Circus)</br>
</br>
음원의 저작권은 아래를 따르고 있으며 지켜져야 합니다.</br>
https://creativecommons.org/licenses/by-nc-sa/4.0/deed.ko
</br></br>

## 곡 추가하는 방법
프로젝트 내 Assests/Sheet 디렉토리에 폴더(name) 및 파일(name.jpg / name.mp3 / name.sheet) 생성하면 됩니다.</br>
폴더 및 파일들은 확장자를 제외하고 반드시 이름이 동일해야 합니다.</br>
파일들의 확장자는 위에서 제시한 것을 따라야 합니다.</br>
</br>
## Editor Hotkey
스페이스 - 재생/일시정지 ( Space - Play/Puase )<br/>
마우스좌클릭 - 노트 배치 ( Mouse leftBtn - Dispose note )<br/>
마우스우클릭 - 노트 삭제 ( Mouse rightBtn - Cancel note )<br/>
마우스휠 - 음악 및 그리드 위치 이동 ( Mouse wheel - Move music and grids pos )<br/>
컨트롤 + 마우스휠 - 그리드 스냅 변경 ( Ctrl + Mouse wheel - Change snap of grids )<br/>
</br>

## 노트 생성 방식에 대해
게임</br>
유니티 자체 ObjectPool을 활용하여 노트를 해당 시점에 필요한만큼 생성/해제.</br>
생성은 마디단위로 이루어지고, 해제는 노트가 화면 밖으로 이탈(노트의 y좌표가 0보다 작으면)하면 해제.</br>
노트는 자신의 위치를 실시간으로 보정하기위해 노력(?)함.</br>
</br>
에디터</br>
게임에서 사용하던 ObjectPool 시스템을 그대로 활용하나, 실행 시 노트를 한번에 전부 생성.</br>
노트를 배치하면, ObjectPool이 현재 사용하지 않는 오브젝트를 재활용(ObjectPool.Get())하거나 새로 생성.</br>
노트를 삭제하면, 해당 오브젝트를 바로 회수하지는 않고 비활성화(gameObject.SetActive(false)).</br>
</br>

## 잘모르겠어요
1.0 버전에서는 리듬게임 제작 방식에 대한 문서가 준비되어 있습니다.</br>
</br>

## 여담
이해하기 쉬운 코드를 약속드렸지만 지켜지진 못한 것 같습니다.
</br>원 계획은 5월초에 시작하여 6월중순에 마칠 예정이었습니다만 개인사정으로 티스푼 코딩을 하다보니 이렇게 됐습니다.</br>
* 마치고 나서 프로젝트를 바라보았을 때 소감은..</br>
굉장히 실험적인 빌드인 것 같습니다. 유니티의 새로운 기능인 Input System, ObjectPool을 사용해보게 되었습니다.</br>
그리고 게임과 에디터가 서로 다른 노트 생성 방식을 사용합니다.</br>
* 아쉬웠던 점은..</br>
실험도 많이했고 생각보다 시간을 많이 사용했기 때문에, 일정상 처음 기획했던 기능들이 많이 제거가 되었습니다.</br>
이후 3.0을 시작하게 된다면 발전 된 모습으로 돌아올 수 있도록 하겠습니다.</br>
*  기타</br>
노트는 1.0과는 다르게 여기서 제작된 에디터로 제가 직접 찍었습니다.</br>
전문 제작자는 아니기에 퀄리티는 떨어집니다 ㅠㅠ (그래도 점점 늘긴 늘더라고요?)</br>
</br>
마지막으로</br>
누군가에겐 이 코드가 도움이 되길 바랍니다.</br>
