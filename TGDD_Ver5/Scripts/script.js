
tinymce.init({
    selector: 'textarea#info',
    height: 300,  // Chiều cao của trình soạn thảo văn bản
    plugins: 'advlist autolink lists link image charmap print preview anchor searchreplace visualblocks code fullscreen insertdatetime media table paste',
    toolbar: 'undo redo | formatselect | bold italic backcolor | alignleft aligncenter alignright alignjustify bullist numlist outdent indent | removeformat | help',
    menu: {
        favs: { title: 'menu', items: 'code visualaid | searchreplace | emoticons' }
    },
    menubar: 'favs file edit view insert format tools table',
    content_style: 'body { font-family: Helvetica, Arial; font-size: 16px; }'
});

function ChangeImage(UploadImage, previewImg) {
    if (UploadImage.files && UploadImage.files[0])
    {
        var reader = new FileReader();
        reader .onload = function (e)
        {
            $(previewImg).attr('src', e.target.result);
        }
        reader.readAsDataURL(UploadImage.files[0]);
    }
}

function ChangeImage(UploadImage, previewImg1) {
    if (UploadImage.files && UploadImage.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImg).attr('src', e.target.result);
        }
        reader.readAsDataURL(UploadImage.files[0]);
    }
}




window.addEventListener('scroll', function () {
    const scrollPosition = window.scrollY;
    const leftContent = document.querySelector('.left-content');
    leftContent.style.transform = `translateX(-${scrollPosition}px)`;
});
/*Slider Prudct*/
var filtered = false;

$('.js-filter').on('click', function () {
    if (filtered === false) {
        $('.filtering').slick('slickFilter', ':even');
        $(this).text('Unfilter Slides');
        filtered = true;
    } else {
        $('.filtering').slick('slickUnfilter');
        $(this).text('Filter Slides');
        filtered = false;
    }
});
/*Slide Img*/
$(document).ready(function () {
    $('#carouselExampleRide_newchain').carousel({
        interval: 3000 // Tự động chuyển đổi mỗi 3 giây
    });
});

$(document).ready(function () {
    $('#carouselExampleRide').carousel({
        interval: 3000 // Tự động chuyển đổi mỗi 3 giây
    });
});



//Banner Left and Right
window.addEventListener('scroll', function () {
    var scrollPosition = window.scrollY;

    if (scrollPosition >= 500) {
        document.querySelector('.banner-left').style.display = 'block'; // Hiển thị banner bên trái
        document.querySelector('.banner-right').style.display = 'block'; // Hiển thị banner bên phải
    } else {
        document.querySelector('.banner-left').style.display = 'none'; // Ẩn banner bên trái
        document.querySelector('.banner-right').style.display = 'none'; // Ẩn banner bên phải
    }
});


function setColor() {
    const color = document.getElementById("colorPicker").value;
    // Sử dụng giá trị màu được chọn ở đây
    console.log("Màu được chọn: " + color);
}

//form for addresscustomer
    document.getElementById("showFormRadio").addEventListener("change", function() {
        var myForm = document.getElementById("myForm");

    if (this.checked) {
        myForm.style.display = "block"; // Hiển thị biểu mẫu khi radio được chọn.
        } else {
        myForm.style.display = "none"; // Ẩn biểu mẫu khi radio không được chọn.
        }
    });



