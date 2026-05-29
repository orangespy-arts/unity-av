# CHANGELOG.md — 变更日志

> Session 之间的接力棒。每次 session 结束前必填。TODO 是最关键的部分。

---

### [2026-05-26] 项目初始化，架构设计

**完成了什么**
- 确定技术方向：Unity URP，实时麦克风，3D穿梭场景
- 确定视觉风格：BC结合（有机自然 + 建筑城市）
- 设计四状态架构：Organic / Architecture / Cosmos / Chaos
- 写好三个核心脚本骨架：AudioAnalyzer / SceneStateManager / PerformanceDirector
- 建立四文件记忆系统

**Next TODO**
- [ ] 查看 EXP-Productions/AudioVizURP 现有脚本，确认哪些可复用
- [ ] 做第一个可跑通的 demo：
  - 麦克风输入正常采集
  - 一条隧道，Bass 驱动顶点变形
  - 摄像机沿隧道自动前进
- [ ] 把三个骨架脚本放进项目，确认编译无报错

---

### [2026-05-26] Session 2 — 项目迁移到 Unity 6 + 六个脚本完成

**完成了什么**
- 将项目迁移至 Unity 6000.3.9f1，URP 升级至 17.3.0，材质自动升级通过
- 查看 EXP-Productions 原仓库：只有 `FrequencyBandAnalyser.cs` 可参考，无可直接复用的摄像机/隧道/状态机代码
- 修复 `AudioAnalyzer.cs`：忙等改协程、频段修正（20/200/2000/20000Hz）、加 Lerp 平滑、indexMax 加 Clamp
- 完成 `SceneStateManager.cs`：状态枚举 Organic/Architecture/Cosmos/Chaos，`GetStateWeight()` 供视觉系统读取
- 完成 `PerformanceDirector.cs`：1/2/3/4 键触发状态切换
- 完成 `TunnelGenerator.cs`：程序化圆柱隧道 mesh，N圈 × M段，法线朝内
- 完成 `TunnelDeformer.cs`：Bass 驱动径向顶点位移 + Z轴传播波
- 完成 `CameraRail.cs`：沿 Z 轴自动前进，RMS 调制速度，到头循环

**已知问题 / 待对齐**
- 脚本实际位置：`Assets/_Project/Scripts/`（扁平），与 ARCHITECTURE.md 规划的子文件夹结构不符
- 命名差异：`CameraRail.cs` vs 规划中的 `CameraController.cs`；隧道拆为两个脚本 vs 规划的 `TunnelChunkSystem.cs`
- 脚本写好但未在 Unity 内搭好 Scene，尚未跑通验证

**Next TODO**
- [ ] 决定：保留扁平结构 or 按 ARCHITECTURE.md 重组文件夹，同步更新 ARCHITECTURE.md
- [ ] 在 Unity 内搭第一个 demo Scene（参考上一 session 的搭法说明）
- [ ] Play Mode 验证：麦克风采集 → 隧道变形 → 摄像机前进
- [ ] 验证后开始 Chunking 系统（无限延伸感）

---

### [2026-05-26] Session 3 — Scene 搭建 + 调试

**完成了什么**
- 目录结构重组：Scripts / Scene / Notes / _Project 分离，ARCHITECTURE.md 同步更新
- Scene `Mainperformance.unity` 创建完成，全部 GameObject 搭好
- 修复回声问题：`audioSource.volume = 0f` 静音输出，FFT 数据不受影响
- `PerformanceDirector` 加 null 检查，防止 SceneStateManager 未初始化时崩溃
- `TunnelGenerator` 改用 `sharedMesh`，防止 Unity 内部复制 mesh 导致变形失效
- `TunnelDeformer` 加 debug 日志（每秒打印 Bass 值）
- `AudioAnalyzer` 移除 `DontDestroyOnLoad`（单场景工具不需要，且导致 Editor 多次 Play 时 Instance 失效）
- 麦克风采集正常：`[AudioAnalyzer] Using mic: 麦克风阵列 (Realtek(R) Audio)`
- 摄像机前进正常，回声已消除，键盘切换待验证

**未解决问题**
- `TunnelDeformer` 仍报 `AudioAnalyzer.Instance is null`，移除 DontDestroyOnLoad 后未能验证是否解决
- 隧道变形尚未跑通

**下一 session 调试方案**
- 如果 Instance 仍为 null：改用 Inspector 直接引用替代 static Instance，彻底绕开初始化顺序问题
  - TunnelDeformer 加 `[SerializeField] AudioAnalyzer audioAnalyzer;`，在 Inspector 手动拖入
- 确认 Bass 实际数值，按需调整 bassScale（FFT raw 值极小，可能需要 500~2000）

---

<!-- 最新的放最上面 -->
