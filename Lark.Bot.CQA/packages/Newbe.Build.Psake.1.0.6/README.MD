# 基于Psake进行自动化构建脚本框架

## 基本步骤

1. 通过nuget安装Newbe.Build.Psake

  ```cmd
  nuget install Newbe.Build.Psake -OutputDirectory packages
  nuget install NuGet.CommandLine -OutputDirectory packages -Version 4.3.0 
  ```

2. 复制nuget包中的Content文件夹到更目录，包含以下文件

  - buildTools\7z.exe
  - buildTools\nuget.exe
  - buildTools\packages.config
  - build.bat
  - build.ps1

3. 按照psake规范对build.ps1进行修改

4. 执行build.bat便可以执行

## 其他说明

### vs2017支持

需要在psake脚本初始化中调用以下函数

```powershell
    Initialize-MSBuild
```
