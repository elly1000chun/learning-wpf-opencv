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
- `Super Resolution` 버튼을 통해 현재 표시 이미지에 AI 기반 Super Resolution 적용

## 화면 구성
<img width="1570" height="1182" alt="image" src="https://github.com/user-attachments/assets/b4cdc764-6e65-41dd-9427-3fd88f1b7024" />

## 빌드 방법

터미널에서 루트 폴더로 이동한 뒤 다음 명령을 실행합니다.

```powershell
dotnet build
```

Visual Studio에서는 솔루션 또는 프로젝트를 연 뒤 `Build > Build Solution`을 실행하면 됩니다.

## 실행 방법

터미널에서 다음 명령을 실행합니다.

```powershell
dotnet run --project .\src\learning-wpf-opencv.csproj
```

Visual Studio에서는 `F5` 또는 `Ctrl + F5`로 실행할 수 있습니다.

## 사용 패키지

```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.2" />
<PackageReference Include="OpenCvSharp4.Windows" Version="4.13.0.20260627" />
```

## AI 모델 파일

Super Resolution 기능은 `EDSR_x4.pb` 모델 파일을 사용합니다.

모델 파일은 Git submodule로 연결된 `Saafke/EDSR_Tensorflow` repository에서 가져옵니다.

```text
resources\EDSR_Tensorflow\models\EDSR_x4.pb
```

submodule 원본은 다음 GitHub repository입니다.

```text
https://github.com/Saafke/EDSR_Tensorflow.git
```

repository를 처음 clone할 때는 submodule까지 함께 가져와야 합니다.

```powershell
git clone --recurse-submodules <repository-url>
```

이미 clone한 repository라면 다음 명령으로 submodule을 초기화합니다.

```powershell
git submodule update --init --recursive
```
