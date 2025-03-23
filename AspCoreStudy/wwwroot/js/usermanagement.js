$(document).ready(function () {
    let currentPage = 1; // 当前页
    let pageSize = 10; // 默认每页显示 10 条
    let totalCount = 0; // 总记录数
    const token = localStorage.getItem("token");

    // 获取用户数据并渲染表格
    function loadUsers(username = "", page = 1, pageSize = 10) {
        $.ajax({
            url: `/api/v1/backstageapi/users?username=${username}&page=${page}&pageSize=${pageSize}`,
            type: 'GET',
            headers: token ? { "Authorization": `Bearer ${token}` } : {},
            success: function (response) {
                totalCount = response.totalCount; // 更新总记录数
                const userTableBody = $('#userTableBody');
                userTableBody.empty(); // 清空表格内容

                response.data.forEach(user => {
                    const row = `
                        <tr>
                            <td>${user.username}</td>
                            <td>${user.email}</td>
                            <td>${user.role}</td>
                            <td>
                                <a class="btn btn-primary btn-sm" href="/Backstage/Edit/${user.id}">编辑</a>
                                <button class="btn btn-danger btn-sm delete-user-btn" data-user-id="${user.id}" data-username="${user.username}">删除</button>
                                <button class="btn btn-warning btn-sm change-password-btn" data-user-id="${user.id}">生成密码</button>
                            </td>
                        </tr>
                    `;
                    userTableBody.append(row);
                });

                // 渲染分页按钮
                renderPagination(totalCount, response.page, response.pageSize);
            },
            error: function () {
                alert('无法加载用户数据，请稍后重试！');
            }
        });
    }

    // 渲染分页按钮
    function renderPagination(totalCount, currentPage, pageSize) {
        const totalPages = Math.ceil(totalCount / pageSize);
        const paginationContainer = $('#pagination');
        paginationContainer.empty(); // 清空分页按钮
    
        const maxVisiblePages = 5; // 最多显示的页码数量
    
        // 添加“上一页”按钮
        if (currentPage > 1) {
            paginationContainer.append(`<button class="btn btn-secondary me-1 pagination-btn" data-page="${currentPage - 1}">上一页</button>`);
        }
    
        // 计算页码范围
        let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
        let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);
    
        // 如果页码范围不足 maxVisiblePages，则调整 startPage
        if (endPage - startPage + 1 < maxVisiblePages) {
            startPage = Math.max(1, endPage - maxVisiblePages + 1);
        }
    
        // 添加第一页和省略号
        if (startPage > 1) {
            paginationContainer.append(`<button class="btn btn-secondary me-1 pagination-btn" data-page="1">1</button>`);
            if (startPage > 2) {
                paginationContainer.append(`<span class="btn btn-light me-1 disabled">...</span>`);
            }
        }
    
        // 添加中间页码
        for (let i = startPage; i <= endPage; i++) {
            const buttonClass = i === currentPage ? 'btn-primary' : 'btn-secondary';
            paginationContainer.append(`<button class="btn ${buttonClass} me-1 pagination-btn" data-page="${i}">${i}</button>`);
        }
    
        // 添加最后一页和省略号
        if (endPage < totalPages) {
            if (endPage < totalPages - 1) {
                paginationContainer.append(`<span class="btn btn-light me-1 disabled">...</span>`);
            }
            paginationContainer.append(`<button class="btn btn-secondary me-1 pagination-btn" data-page="${totalPages}">${totalPages}</button>`);
        }
    
        // 添加“下一页”按钮
        if (currentPage < totalPages) {
            paginationContainer.append(`<button class="btn btn-secondary pagination-btn" data-page="${currentPage + 1}">下一页</button>`);
        }
    
        // 绑定分页按钮点击事件
        $('.pagination-btn').on('click', function () {
            const page = $(this).data('page');
            currentPage = page;
            const username = $('#searchUsername').val().trim();
            loadUsers(username, page, pageSize);
    
            // 更新跳转页数输入框的值
            $('#jumpToPageInput').val(page);
        });
    
        // 更新跳转页数输入框的值为当前页
        $('#jumpToPageInput').val(currentPage);
    }

    // 加载用户数据
    loadUsers();

    // 搜索按钮点击事件
    $('#searchButton').on('click', function () {
        const username = $('#searchUsername').val().trim();
        currentPage = 1; // 搜索时重置为第一页
        loadUsers(username, currentPage, pageSize);
    });

    // 刷新按钮点击事件
    $('#refreshButton').on('click', function () {
        $('#searchUsername').val(''); // 清空搜索框
        currentPage = 1; // 刷新时重置为第一页
        loadUsers("", currentPage, pageSize);
    });

    // 每页显示条数选择事件
    $('#pageSizeSelect').on('change', function () {
        pageSize = parseInt($(this).val()); // 更新每页显示条数
        currentPage = 1; // 切换条数时重置为第一页
        const username = $('#searchUsername').val().trim();
        loadUsers(username, currentPage, pageSize);
    });

    // 跳转到指定页按钮点击事件
    $('#jumpToPageButton').on('click', function () {
        const page = parseInt($('#jumpToPageInput').val(), 10); // 获取输入的页码
        if (isNaN(page) || page < 1) {
            alert('请输入有效的页码！');
            return;
        }

        const totalPages = Math.ceil(totalCount / pageSize); // 计算总页数
        if (page > totalPages) {
            alert(`页码超出范围！最大页码为 ${totalPages}`);
            return;
        }

        currentPage = page; // 更新当前页
        const username = $('#searchUsername').val().trim(); // 获取搜索框的值
        loadUsers(username, currentPage, pageSize); // 加载指定页的数据
    });
});