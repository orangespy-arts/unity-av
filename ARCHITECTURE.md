# ARCHITECTURE.md — AudioVizURP 架构全景

> 低频更新，结构变了才动。

## 目录结构

```
AudioVizURP/
├── Assets/
│   ├── Scripts/                              # 项目脚本（扁平）
│   │   ├── AudioAnalyzer.cs                  # 麦克风输入，提取 Bass/Mid/High/RMS/OnBeat
│   │   ├── SceneStateManager.cs              # 状态机，管理四个视觉状态 + 过渡权重
│   │   ├── PerformanceDirector.cs            # 键盘触发状态切换（1/2/3/4）
│   │   ├── TunnelGenerator.cs                # 程序化圆柱隧道 mesh（N圈 × M段）
│   │   ├── TunnelDeformer.cs                 # Bass 驱动径向顶点位移
│   │   └── CameraRail.cs                     # 摄像机沿 Z 轴自动前进，RMS 调制速度
│   ├── Scene/
│   │   └── Mainperformance.unity             # 唯一演出场景
│   ├── Notes/                                # 搭建指南
│   │   └── guide-001-demo-scene-setup.md
│   └── _Project/                             # 原仓库素材，留作参考 / 改装用
│       ├── Base/                             # 原仓库基础资源（音频、材质、Prefab、URP配置）
│       │   └── Scripts/
│       │       └── FrequencyBandAnalyser.cs  # 原仓库音频分析脚本，参考用
│       └── Tutorials/                        # 原仓库六个教程 Scene + 脚本
│
├── CLAUDE.md
├── ARCHITECTURE.md
├── DECISIONS.md
└── CHANGELOG.md
```

## 数据流

```
Microphone API
    ↓
AudioAnalyzer.cs
    ↓ Bass / Mid / High / RMS / OnBeat（每帧，已 Lerp 平滑）
SceneStateManager.cs（当前状态 + 过渡权重 GetStateWeight()）
    ↓ 映射规则因状态而异
各 State 的视觉脚本 → Shader 参数 / 粒子系统 / Transform
```

## 四个视觉状态

| 状态 | 风格 | 主要视觉元素 |
|------|------|--------------|
| Organic | 有机自然 | 变形隧道，低频驱动膨胀 |
| Architecture | 建筑骨架 | 几何结构，节拍触发生成 |
| Cosmos | 宇宙开阔 | 粒子散开，速度变慢 |
| Chaos | 混沌叠加 | Organic + Architecture 同时激活 |

## 演出宏观结构

| 时间段 | 状态 | 节奏 |
|--------|------|------|
| 0–5min | Organic | 慢速建立 |
| 5–15min | Architecture | 逐渐增强 |
| 15–25min | Chaos | 高潮 |
| 25–30min | Cosmos | 收尾 |

## 当前进度

- [x] 项目迁移：Unity 6000.3.9f1，URP 17.3.0
- [x] 目录结构整理：Scripts / Scene / Notes / _Project 分离
- [x] AudioAnalyzer.cs — 麦克风 + FFT + 平滑 + 节拍检测
- [x] SceneStateManager.cs — 状态机 + 渐变权重
- [x] PerformanceDirector.cs — 键盘触发
- [x] TunnelGenerator.cs — 程序化隧道 mesh
- [x] TunnelDeformer.cs — Bass 驱动变形
- [x] CameraRail.cs — 摄像机前进
- [x] Mainperformance.unity — 演出场景已创建
- [ ] Play Mode 验证：麦克风 → 隧道变形 → 摄像机前进跑通
- [ ] Chunking 系统（无限延伸感）
- [ ] 四个状态完整视觉
- [ ] 后处理（Bloom / Fog / Motion Blur）
