
//alertboxok
setTimeout(() => {
    const alertBox = document.getElementById('alertBox');
    if (alertBox) {
        alertBox.classList.remove('show');
        alertBox.classList.add('fade');
        setTimeout(() => alertBox.remove(), 500);
    }
}, 3000);