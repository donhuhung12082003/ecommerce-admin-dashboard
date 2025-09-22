function Login() {
    event.preventDefault();
    var username = $('input[name=username]').val();
    var password = $('input[name=password]').val();
    $('h4').text('');
    $('.erroruser').remove();   
    $('.errorpass').remove();

    if (username == "" || username == null) {
        
        $('input[name=username]').after('<span class="erroruser" style="color:red">Vui lòng nhập tên đăng nhập</span>');
        return; 
    }

    if (password == "" || password == null) {
        
        $('input[name=password]').after('<span class="errorpass" style="color:red">Vui lòng nhập mật khẩu</span>');
        return; 
    }
   // $('form').submit();


    $.ajax({
        url: '/Admin/HomeAdmin/DangNhapAjax',
        type: 'POST',
        data: {
            username: username,
            password: password
        },
        success: function (response) {
            if (response.success) {
                window.location.href = "/Admin/HomeAdmin/TrangChuAdmin";
            } else {
                $('input[name=username]').val('');
                $('input[name=password]').val('');
                $('h4').text(response.thongbao);
              
            }
        },
        
    });

    
}