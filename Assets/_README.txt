
WELCOME TO THE TEAM! We hope to achieve great things together.

We keep project related files we created inside "Assets/_Project" folder. 
The reason for that, is Assets folder gets crowded with many third party assets, 
and we (unfortunutely) can not relocate those packages into a separate folder
since some third party assets (unfortunutely) work with constant file paths.

Please refer to 
-C# Coding guideline (), 
-Technical Art document (),
-Other Art guidelines 
in Confluence, 
before you contribute.

We use StrangeIOC framework (with Signals instead of Events, as suggested by StrangeIOC), and we utilize MVCS pattern. 
(https://strangeioc.github.io/strangeioc/)

FOLDER STRUCTURE (https://codepen.io/weizhenye/pen/eoYvye)

Denaria-game
├── Assets
│   ├── _Project
│   │   ├── LoadScreen // Feature Name
│   │   │   ├── Scripts
│   │   │   │   ├── Models/LoadScreenModel.cs
│   │   │   │   ├── Views/LoadScreenView.cs
│   │   │   │   ├── Controllers/LoadScreenCommand.cs
│   │   │   │   └── Services/LoadScreenService.cs
│   │   │   └── Sprite //All 2d assets
│   │   ├── Audio //Everything Audio related
│   │   │   ├── Scripts
│   │   │   ├── SFX
│   │   │   └── BackgroundMusics
│   │   ├── VFX //Everything VFX related
│   │   │   └── VFX related text file, scripts and audio, etc.
│   │   ├── MenuUI
│   │       └── Menu UI related text file, scripts and audio, etc.
│   ├── TextMeshPro
│   ├── DOTween
│   ├── OtherThirdPartyAsset
│   └── OtherThirdPartyAsset
└── Packages







