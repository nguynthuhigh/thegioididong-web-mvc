document.addEventListener('DOMContentLoaded', function () {
    var dropdownManu = document.getElementById('dropdownManu');
    var dropdownContent = document.getElementById('dropdownLinkManu');

    dropdownManu.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkManu.style.display = dropdownLinkManu.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkManu.contains(event.target);
        var isLink = event.target === dropdownManu;

        if (!isClickInside && !isLink) {
            dropdownLinkManu.style.display = 'none';
        }
    });
});
//giá
document.addEventListener('DOMContentLoaded', function () {
    var dropdownPrice = document.getElementById('dropdownPrice');
    var dropdownLinkPrice = document.getElementById('dropdownLinkPrice');

    dropdownPrice.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkPrice.style.display = dropdownLinkPrice.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkPrice.contains(event.target);
        var isLink = event.target === dropdownPrice;

        if (!isClickInside && !isLink) {
            dropdownLinkPrice.style.display = 'none';
        }
    });
});
//nhu cầu 
document.addEventListener('DOMContentLoaded', function () {
    var dropdownDemand = document.getElementById('dropdownDemand');
    var dropdownLinkDemand = document.getElementById('dropdownLinkDemand');

    dropdownDemand.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkDemand.style.display = dropdownLinkDemand.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkDemand.contains(event.target);
        var isLink = event.target === dropdownDemand;

        if (!isClickInside && !isLink) {
            dropdownLinkDemand.style.display = 'none';
        }
    });
});
//màn hình
document.addEventListener('DOMContentLoaded', function () {
    var dropdownScreen = document.getElementById('dropdownScreen');
    var dropdownLinkScreen = document.getElementById('dropdownLinkScreen');

    dropdownScreen.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkScreen.style.display = dropdownLinkScreen.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkScreen.contains(event.target);
        var isLink = event.target === dropdownScreen;

        if (!isClickInside && !isLink) {
            dropdownLinkScreen.style.display = 'none';
        }
    });
});
//cpu
document.addEventListener('DOMContentLoaded', function () {
    var dropdownCPU = document.getElementById('dropdownCPU');
    var dropdownLinkCPU = document.getElementById('dropdownLinkCPU');

    dropdownCPU.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkCPU.style.display = dropdownLinkCPU.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkCPU.contains(event.target);
        var isLink = event.target === dropdownCPU;

        if (!isClickInside && !isLink) {
            dropdownLinkCPU.style.display = 'none';
        }
    });
});
//ram
document.addEventListener('DOMContentLoaded', function () {
    var dropdownRAM = document.getElementById('dropdownRAM');
    var dropdownLinkRAM = document.getElementById('dropdownLinkRAM');

    dropdownRAM.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkRAM.style.display = dropdownLinkRAM.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkRAM.contains(event.target);
        var isLink = event.target === dropdownRAM;

        if (!isClickInside && !isLink) {
            dropdownLinkRAM.style.display = 'none';
        }
    });
});
//ổ cứng
document.addEventListener('DOMContentLoaded', function () {
    var dropdownSSD = document.getElementById('dropdownSSD');
    var dropdownLinkSSD = document.getElementById('dropdownLinkSSD');

    dropdownSSD.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkSSD.style.display = dropdownLinkSSD.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkSSD.contains(event.target);
        var isLink = event.target === dropdownSSD;

        if (!isClickInside && !isLink) {
            dropdownLinkSSD.style.display = 'none';
        }
    });
});
//tính lăng đặc biệt
document.addEventListener('DOMContentLoaded', function () {
    var dropdownSpecialFeatures = document.getElementById('dropdownSpecialFeatures');
    var dropdownLinkSpecialFeatures = document.getElementById('dropdownLinkSpecialFeatures');

    dropdownSpecialFeatures.addEventListener('click', function (e) {
        e.preventDefault();
        // Toggle class để ẩn/hiện nội dung
        dropdownLinkSpecialFeatures.style.display = dropdownLinkSpecialFeatures.style.display === 'none' ? 'block' : 'none';
    });

    // Đóng dropdown khi nhấp ra ngoài
    document.addEventListener('click', function (event) {
        var isClickInside = dropdownLinkSpecialFeatures.contains(event.target);
        var isLink = event.target === dropdownSpecialFeatures;

        if (!isClickInside && !isLink) {
            dropdownLinkSpecialFeatures.style.display = 'none';
        }
    });
});