# learning-wpf-opencv

OpenCV, WPF, MVVM 패턴을 학습하기 위한 데스크톱 이미지 처리 예제 프로젝트입니다.

## 개발 환경

- .NET: `net10.0-windows`
- UI Framework: WPF
- Architecture: MVVM
- OpenCV Wrapper: OpenCvSharp
- MVVM Toolkit: CommunityToolkit.Mvvm

## 주요 기능

- `Open File` 버튼을 통해 이미지 파일 선택
- 선택한 이미지 파일 경로를 상단 TextBox에 표시
- OpenCV로 이미지 파일을 로드한 뒤 WPF Image 영역에 표시
- `Smooth` 버튼을 통해 현재 표시 이미지에 Gaussian Blur 적용

## 화면 구성


## 빌드 방법

터미널에서 프로젝트 폴더로 이동한 뒤 다음 명령을 실행합니다.

```powershell
dotnet build
```

Visual Studio에서는 솔루션 또는 프로젝트를 연 뒤 `Build > Build Solution`을 실행하면 됩니다.

## 실행 방법

터미널에서 다음 명령을 실행합니다.

```powershell
dotnet run
```

Visual Studio에서는 `F5` 또는 `Ctrl + F5`로 실행할 수 있습니다.

## 사용 패키지

```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.2" />
<PackageReference Include="OpenCvSharp4.Windows" Version="4.13.0.20260627" />
```