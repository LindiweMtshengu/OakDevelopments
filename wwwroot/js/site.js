document.addEventListener("DOMContentLoaded", function () {

    const modal = document.getElementById("imageModal");
    const modalScroll = document.getElementById("modalScroll");
    const closeBtn = document.querySelector(".close-btn");

    document.querySelectorAll(".details-gallery img").forEach(img => {

        img.addEventListener("click", function () {

            const gallery = this.closest(".property-gallery");
            const images = gallery.querySelectorAll("img");

            modal.style.display = "flex";
            modalScroll.innerHTML = "";

            // Add ALL images vertically
            images.forEach(i => {
                const fullImg = document.createElement("img");
                fullImg.src = i.src;
                modalScroll.appendChild(fullImg);
            });

        });

    });

    closeBtn.addEventListener("click", () => {
        modal.style.display = "none";
    });


    modal.addEventListener("click", function (e) {
        if (e.target === modal) {
            modal.style.display = "none";
        }
    });

});