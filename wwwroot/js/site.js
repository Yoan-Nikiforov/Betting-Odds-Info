document.addEventListener("DOMContentLoaded", function () {
    let btns = document.querySelectorAll("button.reveal-markets-btn")
    if (btns.length === 0) return
    btns.forEach(btn => btn.addEventListener('click', function (e) {
        e.preventDefault();
        btn.classList.toggle('active')
        let markets = btn.parentElement.parentElement.nextElementSibling
        markets.classList.toggle('open')
    }))
})
