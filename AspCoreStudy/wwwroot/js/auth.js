function parseJwt(token) {
    try {
        let base64Url = token.split('.')[1];
        let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        let jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    } catch (error) {
        console.error("解析 JWT 失败:", error);
        return null;
    }
}

function checkAuthentication() {
    let token = localStorage.getItem("token");

    if (!token) {
        window.location.href = "/Account/Login";
        return false;
    }

    return parseJwt(token);
}