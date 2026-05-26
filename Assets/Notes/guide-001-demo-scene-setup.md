# Guide 001 — Demo Scene 搭建

对应 demo：单一隧道 + 麦克风低频驱动变形 + 摄像机自动前进。

---

## 1. 新建 Scene

在 `Assets/_Project/` 右键 → Create → Scene，命名 `MainPerformance`，双击打开。

---

## 2. AudioAnalyzer GameObject

1. Hierarchy 右键 → Create Empty，命名 `AudioAnalyzer`
2. Add Component → `Audio Source`
3. Add Component → `Audio Analyzer`
4. AudioSource 设置：
   - **Play On Awake**: 关掉
   - **Loop**: 勾上

---

## 3. Directors GameObject

1. Create Empty，命名 `Directors`
2. Add Component → `Scene State Manager`
3. Add Component → `Performance Director`

---

## 4. Tunnel GameObject

1. Create Empty，命名 `Tunnel`，Position `(0, 0, 0)`
2. Add Component → `Tunnel Generator`
3. Add Component → `Tunnel Deformer`
4. Add Component → `Mesh Renderer`
5. 新建 Material（右键 Assets → Create → Material）
   - Shader 选 `Universal Render Pipeline/Unlit`
   - 颜色选亮色方便观察
6. 将 Material 拖入 Mesh Renderer 的 Materials[0]

TunnelGenerator 参数：

| 参数 | 值 |
|------|----|
| Rings | 64 |
| Segments | 24 |
| Radius | 3 |
| Length | 80 |

---

## 5. Main Camera

1. Position 设 `(0, 0, -2)`
2. Add Component → `Camera Rail`

CameraRail 参数：

| 参数 | 值 |
|------|----|
| Base Speed | 5 |
| Tunnel Length | 80 |

---

## 6. Play 前检查清单

- [ ] `AudioAnalyzer`：AudioSource + AudioAnalyzer
- [ ] `Directors`：SceneStateManager + PerformanceDirector
- [ ] `Tunnel`：MeshFilter + MeshRenderer + TunnelGenerator + TunnelDeformer + Material 已挂
- [ ] `Main Camera`：CameraRail，Position `(0, 0, -2)`

---

## 7. 运行验证

按 Play，对着麦克风说话或拍手。

**正常现象：**
- Console：`[AudioAnalyzer] Using mic: 你的麦克风名字`
- 隧道内壁可见，随低频鼓动
- 摄像机向前穿行，80 单位后循环

**隧道不可见：** 确认 Material 已挂，或将 Camera Position Z 改为 `5` 向里移动后再观察。

**键盘控制：**
- `1` → Organic
- `2` → Architecture
- `3` → Cosmos
- `4` → Chaos
