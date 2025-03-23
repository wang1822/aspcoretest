document.addEventListener("DOMContentLoaded", function () {

    let payload = checkAuthentication();

    if (payload && payload.unique_name)
        document.getElementById("usernameDisplay").textContent = payload.unique_name.charAt(0);

    const hasManagementPermission = payload?.permissions?.includes("Management");
    document.getElementById("userManagementItem").style.display = hasManagementPermission ? "block" : "none";
    document.getElementById("logViewItem").style.display = hasManagementPermission ? "block" : "none";

    initializeTheme()
});

function initializeTheme() {
    const body = document.body;
    const themeToggle = document.getElementById("themeToggle");

    // 从 localStorage 获取保存的主题
    let savedTheme = localStorage.getItem("theme");
    if (!savedTheme) {
        savedTheme = "light-mode"; // 默认主题为亮色模式
        localStorage.setItem("theme", savedTheme);
    }

    // 应用保存的主题
    body.classList.add(savedTheme);

    // 如果存在 themeToggle 按钮，则绑定切换事件
    if (themeToggle) {
        themeToggle.addEventListener("click", function () {
            if (body.classList.contains("light-mode")) {
                body.classList.remove("light-mode");
                body.classList.add("dark-mode");
                localStorage.setItem("theme", "dark-mode");
            } else {
                body.classList.remove("dark-mode");
                body.classList.add("light-mode");
                localStorage.setItem("theme", "light-mode");
            }
        });
    }
}