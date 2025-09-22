function previewImage(event) {
    const file = event.target.files[0];
    const preview = document.getElementById("preview");

    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            preview.src = e.target.result;
            preview.style.display = "block";
        };
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
        preview.style.display = "none";
    }
}

function xoaanh() {
    const input = document.getElementById("fileInput");
    const preview = document.getElementById("preview");

    // reset input file
    input.value = "";

    // reset preview
    preview.src = "";
    preview.style.display = "none";

    $.ajax({
        url: '/Admin/SanPham/XoaAnh',
        type: 'POST',
        data: { id: $("#ID").val() }, 
        success: function (response) {
            console.log('Ảnh đã được xóa trên server.');
        },
        error: function (error) {
            console.error('Lỗi khi xóa ảnh trên server:', error);
        }
    });
}
function confirmDelete(id) {
    if (confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?") == true) {
        location.href = "/Admin/SanPham/Xoa?id=" + id;
    };
}

function xoaKhachHang(id) {
    if (confirm("Bạn có chắc chắn muốn xóa khách hàng này không?") == true) {
        location.href = "/Admin/KhachHang/Xoa?id=" + id;
    };
}

function xoaChiTietDonHang(id) {
    if (confirm("Bạn có chắc chắn muốn xóa sản phẩm này không?") == true) {
        location.href = "/Admin/DonHang/XoaChiTietDonHang?id=" + id;
    };
}

