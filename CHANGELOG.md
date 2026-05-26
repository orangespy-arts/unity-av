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

<!-- 最新的放最上面 -->
