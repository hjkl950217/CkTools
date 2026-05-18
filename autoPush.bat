@echo off
chcp 65001


echo 拉取最新代码...
git pull

echo 添加所有修改...
git add .

echo 检查当前状态...
git status > status.log

findstr "Changes to be committed" status.log > nul
if %errorlevel% equ 0 (
    echo 发现有文件改动。
    echo 是否继续提交并推送？ (Y/N) 
    set /p "continue="
    echo.
    if /i "%continue%"=="y" (
        echo 正在提交代码...
        git commit -m "更新文件"
        echo 正在推送代码...
        git push
        echo 操作完成。
        del status.log 2>nul
        goto :end
    ) else if /i "%continue%"=="n" (
        echo 已取消提交和推送。
        del status.log 2>nul
        goto :end
    ) else (
        echo 输入无效，已取消提交和推送。
         del status.log 2>nul
         goto :end
    )
) else (
    echo 没有发现需要提交的改动。
     del status.log 2>nul
)

:end
pause
exit /b 0