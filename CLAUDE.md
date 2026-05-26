# AudioVizURP — CLAUDE.md

## 项目

Unity URP 实时音画演出项目。麦克风输入驱动3D视觉，用于30分钟现场 AV performance。
基于 https://github.com/EXP-Productions/AudioVizURP 扩展开发。

## 读取顺序

每次 session 开始前，按顺序读取：
1. CLAUDE.md（本文件）
2. ARCHITECTURE.md
3. DECISIONS.md
4. CHANGELOG.md（重点看 Next TODO）

## 核心约定

- 渲染管线：URP，Unity 6000.3.9f1
- 语言：C#
- 场景结构：单一 Scene，状态机管理视觉状态
- 音频：实时麦克风，AudioAnalyzer 统一提供数据
- 不讨论已在 DECISIONS.md 否决的方案

## 死令

每次 session 结束前必须更新 CHANGELOG.md。
