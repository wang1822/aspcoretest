$(document).ready(function () {
    $('#registerForm').submit(function (event) {
        event.preventDefault();

        const username = $('#username').val();
        const password = $('#password').val();
        const email = $('#email').val();

         const requestData = {
            username: username,
            passwordHash: password
        };

        if (email) 
            requestData.email = email;

        sendAjaxRequest(
            '/api/v1/accountapi/register',
            'POST',
            requestData,
            function (response) {
                alert('注册成功');
                window.location.href = '/Account/Login';
            },
            function (xhr) {
                const errors = xhr.responseJSON?.errors;
                if (xhr.status === 500) {
                    alert('服务器错误，请稍后再试');
                } else {
                    let errorMessages = [];
                    $.each(errors, function (field, messages) {
                        errorMessages.push(messages.join(', '));
                    });
                    alert('\n' + errorMessages.join('\n'));
                }
            }
        );
    });
});