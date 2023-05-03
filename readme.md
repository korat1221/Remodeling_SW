
1. SW 컴포넌트 구성도

![제목 없는 다이어그램 drawio](https://user-images.githubusercontent.com/128672029/234760868-7a1a0023-c229-4baa-9ae3-8911a076e078.png)

2. 작업환경 설정 방법

  (1) 이 소스를 클론한다.
  
      폴더 구조는 아래와 같다.
      
![image](https://user-images.githubusercontent.com/128672029/234761654-84d4cce9-045e-48ad-871e-3e33603d053b.png)
  
  (2) https://www.fitterlite.com/apps/si/asset.zip 를 다운로드하여 압축해제한 후 asset 폴더를 클론한 같은 이름의 폴더에 덮어 씌운다.
  
  (3) https://www.fitterlite.com/apps/si/sw.zip 를 다운로드하여 압축해제한 후 main.exe 를 실행하여 각종 컴포넌트를 설치한다. 
  
      이때 실행후 3차원 박스 애니메이션 창이 실행되는지까지 확인한다.

  (4) 비주얼 스튜디오 2022 커뮤니티 버전(64비트)을 PC 에 설치하고 main.sln 을 로드한다.
  
  (5) main C# 프로젝트에서 작업한다.
  
      개발은 Debug 모드에서 하시기 바랍니다. Release 모드는 제품화 코드가 추가되어 디버깅이 용이하지 않을것 같습니다. 

3. 기초 데이터의 데이터베이스화 작업 단계

  (1) 원본 엑셀 파일은 기초 데이터 폴더의 엑셀 파일들과 같이 하나의 파일이 하나의 데이터베이스가 되는 형태이다. 기초 데이터 폴더의 엑셀 파일들을 참조하여 유사한 로우, 컬럼 구조로 원본 엑셀 파일을 편집한다. 엑셀 파일 내 시트의 개수는 한개이다.
  
  (2) 원본 엑셀 파일을 탭으로 분리 (txt) 파일로 저장한다.
  
  (3) 탭으로 분리 파일의 인코딩을 UTF-8 로 변환한다.(노트패드 기능 활용할것)
  
  ![image](https://user-images.githubusercontent.com/128672029/235830326-6c964403-d5cc-4e04-ade3-f6cdbac4b789.png)
  
  (4) dbmaker 프로그램을 실행시키고 아래 이미지를 참조하여 데이터베이스 변환한다.
  
  ![image](https://user-images.githubusercontent.com/128672029/235831351-2c2b1317-4b24-4211-88a2-1cb422cff29b.png)

