# UnityRhythmGame

오랜만입니다. <br/>
프로젝트를 새롭게 시작하였습니다. <br/>
브랜치에서 2.0을 선택하면 작업중인 모습을 확인하실 수 있습니다. <br/>
새롭게 만든 이유는 그냥 취미입니다. <br/>
기존 코드는 학부 시절에 정말 리듬게임이 너무 만들어보고 싶어, 좋은 코드보다 일단 만들자였기 때문에<br/>
지금 제가 보기에도 남들이 보기에도 너무나 이해하기 어려운 코드인 것 같습니다.<br/>
다소 복잡한 개념인 timeSamples까지 들어가 있고, 게다가 사실 제대로 활용한 것 같지도 않네요.<br/>
2.0 에서는 게임과 에디터를 동시에 마주칠 수 있을 것이며 조금 더 이해하기 쉬운 프로그램을 만들기 위해 노력하겠습니다.<br/>
2022년 5월 7일 LHEALP 작성.<br/>


## 중요! Important!

https://github.com/LHEALP/Runthm <br/>
노트 에디터 프로젝트가 **공개**되었습니다! <br/>
Note editor project has been **public**!<br/>
<br/>
오랜만입니다.<br/>
노트 에디터를 공개할 예정입니다. https://github.com/LHEALP/Runthm <br/>
11월부터 프로젝트를 새롭게 생성하여 조금씩 작업하였으며 어느정도 큰 틀은 완성된 상태입니다.<br/>
현재 프로젝트와 병합을 시도하려 했으나 여러 문제가 예측되어 아쉽게도 취소하였습니다.<br/>
그리고 프로젝트 변경사항으로 인해 문서가 일치하지 않는 부분이 생겼을 것으로 보이나(SongSelect 씬), 오히려 사용법이 매우 간소화 되었기 때문에 큰 상관은 없을 것입니다.<br/>
<br/>
Long time no see.<br/>
I'm going to release a note editor. https://github.com/LHEALP/Runthm <br/>
From November, a new project was created and worked little by little, and a somewhat large frame was completed.<br/>
I tried to merge with the current project, but it was unfortunately canceled because several problems were predicted.<br/>
And it seems that there are some inconsistencies in the documentation due to changes in the project (SongSelect scene), but it doesn't matter much because the usage is very simplified.<br/>


## 프로젝트 업데이트 Project Update
+ 유니티 버전업 2019.3.10f1 에서 2019.4.9f1
+ 음악 선택씬의 리스트뷰 자동화. Resources 폴더 내의 항목들을 읽어 알아서 생성하도록 변경.
+ 에디터와 관련해, 시트 포맷이 일부 변경되어 구별하기 위한 코드 추가. (기존 시트도 여전히 사용 가능)
+ 가독성을 떨어트리는 의미없는 주석 및 코드 최소화
+ 여담.. 이 코드 누가 짠거야! 어? 나네? 😂

+ upgrade unity from 2019.3.10f1 to 2019.4.9f1
+ Automated list view of music selection scene. Changed to automatically create by reading items in the Resources folder.
+ Regarding the editor, the sheet format has been partially changed, so the code to distinguish it has been added. (Existing sheets can still be used)
+ Minimize meaningless comments and code that degrade readability
+ Aside... who wrote this code! uh? Me? 😂

Doc(Eng) used a translator.<br/>
https://translate.kakao.com/<br/>
https://translate.google.co.kr/<br/>


## bgm provider 'SHK'
https://blog.naver.com/soundholick

+ Whisper of the summer night (acoustic ver.)
https://youtu.be/I4Yox9jqRb0

+ Milky Way
https://youtu.be/J6N3cy2LNEk


## Play Video

[![Watch the video](https://img.youtube.com/vi/WKaMFs3Du6g/0.jpg)](https://youtu.be/WKaMFs3Du6g)


*The music used in the video below did not be uploaded to Github due to copyright issues.*

[![Watch the video](https://img.youtube.com/vi/T_xeteBYZ88/0.jpg)](https://youtu.be/T_xeteBYZ88)

[![Watch the video](https://img.youtube.com/vi/GhWhDXBq6aM/0.jpg)](https://youtu.be/GhWhDXBq6aM)


<br/>
유니티엔진 리듬게임(노트방식)<br/>

스크립트는 씬별로 폴더마다 저장되어 있습니다.<br/>
리듬게임의 핵심은 Play에 담겨있습니다.(노트 생성 및 판정등)<br/>
~SongSelect 폴더의 일부 스크립트는 https://youtu.be/GpPWbM_6DVY 의 도움을 받아 작성되었습니다.<br/>
급하게 리스트뷰가 필요하다보니 완전히 시스템에 부합하는 코드가 되진 못했습니다. 이 시스템에서 약간의 비효율을 가지고 있는 부분입니다.~<br/>
**기존 설계는 그대로 가져가지만, 수동 작업이 필요했던 부분을 자동화 하였습니다.**<br/>



## 주저리 blah blah

유니티엔진으로 리듬게임 개발하는데 조금이나마 도움이 되었으면 하고자 업로드하게 되었습니다.<br/>
저는 리듬게임을 실제로 플레이에 시간을 쏟아 부은적이 있어 리듬게임의 요구사항을 상대적으로 많은 것을 알고 있었으나<br/>
이를 개발하고자 할 때, 어떤식으로 작성해야 하는지 어떻게 해야 맞는 것인지 알 길이 없었기 때문에 여러 시행착오를 겪었습니다.<br/>
"노트는 위에서 아래로 떨어진다." 부터 시작해서 모든 동작을 작은 단위로 나누었고 구현해 나가기 시작했습니다.<br/>
하다가 막히면 구글링을 하고 반복. 단번의 검색에 필요한 정보가 나오진 않았지만 어떤 것이 해결의 실마리가 되기도 했습니다.<br/>
깃허브, 티스토리, 스택오버플로우등 질문을 하기 위해 올려놓은 코드 조차도 필요한 정보가 될 수 있었습니다.<br/>
이 것이 업로드를 결정하게 된 가장 큰 이유입니다.<br/>
정말 성의 없이 올려놔도 누군가에겐 도움이 될 수 있구나 라는 것을 느꼈습니다.<br/>

이 코드가 어떤 이에겐 별 볼일 없어도, 누군가에겐 도움이 되었으면 좋겠습니다.<br/>

LHEALP 작성<br/>

