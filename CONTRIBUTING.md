# 开发指南

## 使用 Visual Studio

本项目是一个普通的 WinForm 项目，普通地使用 Visual Studio 开发。

Debug 模式还需要部署并启动 [ks233/ja-learner-webview](https://github.com/ks233/ja-learner-webview)。

## 使用 VS Code

### 在 Dev Container 中编译

1. **打开项目**：在 VS Code 中打开 `ja-learner` 项目。

2. **打开远程容器**：点击左下角的绿色按钮（通常显示为 "><" 或 "打开远程窗口"），然后选择 "Reopen in Container"。(确保Docker已安装并运行, )

3. **等待容器构建**：VS Code 会自动下载并构建 devcontainer，这可能需要一些时间。

4. **运行编译命令**：
   
   ```bash
   dotnet publish ja-learner.sln -c Release -r win-x64 --self-contained
   ```

5. **构建 Vue.js 项目**：
   
   ```bash
   cd ..
   git clone https://github.com/ks233/ja-learner-webview
   cd ja-learner-webview
   npm install
   npm run build
   mv dist ../ja-learner/bin/Release/net6.0-windows/win-x64
   ```

6. **完成编译**：编译完成后，你可以在 `bin/Release/net6.0-windows/win-x64` 目录下找到编译后的exe可执行文件。
