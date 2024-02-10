document.addEventListener("DOMContentLoaded", function () {
    const colorRadios = document.querySelectorAll('input[name="color"]');
    const confirmButton = document.getElementById('confirmButton');
    const outputDiv = document.getElementById('output');

    confirmButton.addEventListener('click', function () {
        const selectedColorRadio = document.querySelector('input[name="color"]:checked');

        if (selectedColorRadio) {
            const selectedColor = selectedColorRadio.value;
            outputDiv.textContent = `Selected color: ${selectedColor}`;
            outputDiv.style.color = selectedColor;
        } else {
            outputDiv.textContent = "Please select a color";
        }
    });
});

function enableSubmit() {
    // Lấy ra element nút submit
    var submitButton = document.getElementById('submitButton');

    // Kiểm tra nếu một ô radio đã được chọn
    if (document.querySelector('input[name="PaymentMethod"]:checked')) {
        // Bật chức năng submit
        submitButton.disabled = false;
    }
}
