document.addEventListener("DOMContentLoaded", function () {
    // Lấy tất cả các phần tử có class là "gia" và "giaCu"
    var giaElements = document.querySelectorAll(".gia");
    var giaMoiElements = document.querySelectorAll(".giaMoi");
    var giaLenDoiElements = document.querySelectorAll(".giaLenDoi");

    // Duyệt qua từng phần tử và định dạng giá tiền
    giaElements.forEach(function (element) {
        var gia = parseFloat(element.textContent);
        var formattedGia = gia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        element.textContent = formattedGia;
    });

    giaMoiElements.forEach(function (element) {
        var gia = parseFloat(element.textContent);
        var formattedGia = gia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        element.textContent = formattedGia;
    });

    giaLenDoiElements.forEach(function (element) {
        var gia = parseFloat(element.textContent);
        var formattedGia = gia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        element.textContent = formattedGia;
    });



    const filledHearts = document.querySelectorAll('.filledHeart');
    const emptyHearts = document.querySelectorAll('.emptyHeart');

    filledHearts.forEach(function (filledHeart, index) {
        filledHeart.addEventListener('click', function () {
            filledHeart.style.display = 'none';
            emptyHearts[index].style.display = 'block';
        });
    });

    emptyHearts.forEach(function (emptyHeart, index) {
        emptyHeart.addEventListener('click', function () {
            emptyHeart.style.display = 'none';
            filledHearts[index].style.display = 'block';
        });
    });

    // Lấy thẻ button và thêm sự kiện click để di chuyển lên đầu trang
    var backToTopButton = document.getElementById('backToTop');

    // Hiển thị hoặc ẩn nút khi người dùng cuộn trang
    window.addEventListener('scroll', function () {
        if (window.scrollY > 300) { // Nếu vị trí cuộn lớn hơn 300px
            backToTopButton.classList.add('show');
        } else {
            backToTopButton.classList.remove('show');
        }
    });

    // Thực hiện cuộn lên đầu trang khi click vào nút
    backToTopButton.addEventListener('click', function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
});

// Your JavaScript logic to update the countdown timer
function countdown() {
    const endDate = new Date("2023-12-01T00:00:00Z"); // Set your end date here
    const now = new Date().getTime();
    const timeLeft = endDate - now;

    const days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
    const hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);

    document.getElementById("days").innerHTML = days.toString().padStart(2, '0');
    document.getElementById("hours").innerHTML = hours.toString().padStart(2, '0');
    document.getElementById("minutes").innerHTML = minutes.toString().padStart(2, '0');
    document.getElementById("seconds").innerHTML = seconds.toString().padStart(2, '0');

    if (timeLeft < 0) {
        clearInterval(interval);
        document.querySelector('.title').innerHTML = 'Kết thúc';
        document.querySelector('.box-time').style.display = 'none';
    }
}

const interval = setInterval(countdown, 1000);


function getProductsPerRow() {
    if (window.innerWidth <= 786) {
        return 2; // Màn hình nhỏ hơn hoặc bằng 786px, hiển thị 2 sản phẩm mỗi dòng
    } else {
        return 5; // Mặc định hiển thị 5 sản phẩm mỗi dòng
    }
}