document.addEventListener("DOMContentLoaded", function () {
    let token = localStorage.getItem("token");

    if (token) {
        let payload = parseJwt(token);

        if (payload && payload.unique_name) 
            document.getElementById("usernameDisplay").textContent = payload.unique_name;
    }
});