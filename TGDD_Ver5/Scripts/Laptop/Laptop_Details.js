
    function showForm(formId) {
      // Ẩn tất cả các form
      var forms = document.querySelectorAll('.form-group');
    forms.forEach(function(form) {
        form.style.display = 'none';
      });

    // Hiển thị form được chọn
    var selectedForm = document.getElementById(formId);
    selectedForm.style.display = 'block';
}


document.addEventListener('DOMContentLoaded', function () {
    // Lấy đối tượng ảnh có class clickable-image
    var clickableImage = document.querySelector('.clickable-image');

    // Thêm sự kiện click cho ảnh
    clickableImage.addEventListener('click', function () {
        // Scroll xuống vị trí 500px từ đỉnh trang
        window.scrollTo(0, 700);
    });
});

document.addEventListener('DOMContentLoaded', function () {
    // Lấy đối tượng ảnh có class clickable-image
    var clickableImage = document.querySelector('.clickable-image_info');

    // Thêm sự kiện click cho ảnh
    clickableImage.addEventListener('click', function () {
        // Scroll xuống vị trí 500px từ đỉnh trang
        window.scrollTo(0, 1000);
    });
});

