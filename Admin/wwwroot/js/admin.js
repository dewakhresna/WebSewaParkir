/**
 * Template Name: NiceAdmin
 * Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
 * Updated: Apr 20 2024 with Bootstrap v5.3.3
 * Author: BootstrapMade.com
 * License: https://bootstrapmade.com/license/
 */

(function () {
    "use strict";

    /**
     * Easy selector helper function
     */
    const select = (el, all = false) => {
        el = el.trim();
        if (all) {
            return [...document.querySelectorAll(el)];
        } else {
            return document.querySelector(el);
        }
    };

    /**
     * Easy event listener function
     */
    const on = (type, el, listener, all = false) => {
        if (all) {
            select(el, all).forEach((e) => e.addEventListener(type, listener));
        } else {
            select(el, all).addEventListener(type, listener);
        }
    };

    /**
     * Easy on scroll event listener
     */
    const onscroll = (el, listener) => {
        el.addEventListener("scroll", listener);
    };

    /**
     * Sidebar toggle
     */
    if (select(".toggle-sidebar-btn")) {
        on("click", ".toggle-sidebar-btn", function (e) {
            select("body").classList.toggle("toggle-sidebar");
        });
    }

    /**
     * Search bar toggle
     */
    if (select(".search-bar-toggle")) {
        on("click", ".search-bar-toggle", function (e) {
            select(".search-bar").classList.toggle("search-bar-show");
        });
    }

    /**
     * Navbar links active state on scroll
     */
    let navbarlinks = select("#navbar .scrollto", true);
    const navbarlinksActive = () => {
        let position = window.scrollY + 200;
        navbarlinks.forEach((navbarlink) => {
            if (!navbarlink.hash) return;
            let section = select(navbarlink.hash);
            if (!section) return;
            if (
                position >= section.offsetTop &&
                position <= section.offsetTop + section.offsetHeight
            ) {
                navbarlink.classList.add("active");
            } else {
                navbarlink.classList.remove("active");
            }
        });
    };
    window.addEventListener("load", navbarlinksActive);
    onscroll(document, navbarlinksActive);

    /**
     * Toggle .header-scrolled class to #header when page is scrolled
     */
    let selectHeader = select("#header");
    if (selectHeader) {
        const headerScrolled = () => {
            if (window.scrollY > 100) {
                selectHeader.classList.add("header-scrolled");
            } else {
                selectHeader.classList.remove("header-scrolled");
            }
        };
        window.addEventListener("load", headerScrolled);
        onscroll(document, headerScrolled);
    }

    /**
     * Back to top button
     */
    let backtotop = select(".back-to-top");
    if (backtotop) {
        const toggleBacktotop = () => {
            if (window.scrollY > 100) {
                backtotop.classList.add("active");
            } else {
                backtotop.classList.remove("active");
            }
        };
        window.addEventListener("load", toggleBacktotop);
        onscroll(document, toggleBacktotop);
    }

    /**
     * Initiate tooltips
     */
    var tooltipTriggerList = [].slice.call(
        document.querySelectorAll('[data-bs-toggle="tooltip"]')
    );
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    /**
     * Initiate quill editors
     */
    if (select(".quill-editor-default")) {
        new Quill(".quill-editor-default", {
            theme: "snow",
        });
    }

    if (select(".quill-editor-bubble")) {
        new Quill(".quill-editor-bubble", {
            theme: "bubble",
        });
    }

    if (select(".quill-editor-full")) {
        new Quill(".quill-editor-full", {
            modules: {
                toolbar: [
                    [
                        {
                            font: [],
                        },
                        {
                            size: [],
                        },
                    ],
                    ["bold", "italic", "underline", "strike"],
                    [
                        {
                            color: [],
                        },
                        {
                            background: [],
                        },
                    ],
                    [
                        {
                            script: "super",
                        },
                        {
                            script: "sub",
                        },
                    ],
                    [
                        {
                            list: "ordered",
                        },
                        {
                            list: "bullet",
                        },
                        {
                            indent: "-1",
                        },
                        {
                            indent: "+1",
                        },
                    ],
                    [
                        "direction",
                        {
                            align: [],
                        },
                    ],
                    ["link", "image", "video"],
                    ["clean"],
                ],
            },
            theme: "snow",
        });
    }

    /**
     * Initiate TinyMCE Editor
     */

    const useDarkMode = window.matchMedia(
        "(prefers-color-scheme: dark)"
    ).matches;
    const isSmallScreen = window.matchMedia("(max-width: 1023.5px)").matches;

    tinymce.init({
        selector: "textarea.tinymce-editor",
        plugins:
            "preview importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media codesample table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount help charmap quickbars emoticons accordion",
        editimage_cors_hosts: ["picsum.photos"],
        menubar: "file edit view insert format tools table help",
        toolbar:
            "undo redo | accordion accordionremove | blocks fontfamily fontsize | bold italic underline strikethrough | align numlist bullist | link image | table media | lineheight outdent indent| forecolor backcolor removeformat | charmap emoticons | code fullscreen preview | save print | pagebreak anchor codesample | ltr rtl",
        autosave_ask_before_unload: true,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true,
        link_list: [
            {
                title: "My page 1",
                value: "https://www.tiny.cloud",
            },
            {
                title: "My page 2",
                value: "http://www.moxiecode.com",
            },
        ],
        image_list: [
            {
                title: "My page 1",
                value: "https://www.tiny.cloud",
            },
            {
                title: "My page 2",
                value: "http://www.moxiecode.com",
            },
        ],
        image_class_list: [
            {
                title: "None",
                value: "",
            },
            {
                title: "Some class",
                value: "class-name",
            },
        ],
        importcss_append: true,
        file_picker_callback: (callback, value, meta) => {
            /* Provide file and text for the link dialog */
            if (meta.filetype === "file") {
                callback("https://www.google.com/logos/google.jpg", {
                    text: "My text",
                });
            }

            /* Provide image and alt text for the image dialog */
            if (meta.filetype === "image") {
                callback("https://www.google.com/logos/google.jpg", {
                    alt: "My alt text",
                });
            }

            /* Provide alternative source and posted for the media dialog */
            if (meta.filetype === "media") {
                callback("movie.mp4", {
                    source2: "alt.ogg",
                    poster: "https://www.google.com/logos/google.jpg",
                });
            }
        },
        height: 600,
        image_caption: true,
        quickbars_selection_toolbar:
            "bold italic | quicklink h2 h3 blockquote quickimage quicktable",
        noneditable_class: "mceNonEditable",
        toolbar_mode: "sliding",
        contextmenu: "link image table",
        skin: useDarkMode ? "oxide-dark" : "oxide",
        content_css: useDarkMode ? "dark" : "default",
        content_style:
            "body { font-family:Helvetica,Arial,sans-serif; font-size:16px }",
    });

    /**
     * Initiate Bootstrap validation check
     */
    var needsValidation = document.querySelectorAll(".needs-validation");

    Array.prototype.slice.call(needsValidation).forEach(function (form) {
        form.addEventListener(
            "submit",
            function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }

                form.classList.add("was-validated");
            },
            false
        );
    });

    /**
     * Initiate Datatables
     */
    const datatables = select(".datatable", true);
    datatables.forEach((datatable) => {
        new simpleDatatables.DataTable(datatable, {
            perPageSelect: [5, 10, 15, ["All", -1]],
            columns: [
                {
                    select: 2,
                    sortSequence: ["desc", "asc"],
                },
                {
                    select: 3,
                    sortSequence: ["desc"],
                },
                {
                    select: 4,
                    cellClass: "green",
                    headerClass: "red",
                },
            ],
        });
    });

    /**
     * Autoresize echart charts
     */
    const mainContainer = select("#main");
    if (mainContainer) {
        setTimeout(() => {
            new ResizeObserver(function () {
                select(".echart", true).forEach((getEchart) => {
                    echarts.getInstanceByDom(getEchart).resize();
                });
            }).observe(mainContainer);
        }, 200);
    }
})();

//document.addEventListener('DOMContentLoaded', function () {
//    var basicModal = document.getElementById('basicModal');
//    basicModal.addEventListener('show.bs.modal', function (event) {
//        var button = event.relatedTarget; // Tombol yang diklik
//        var id = button.getAttribute('data-id');
//        var paymentProof = button.getAttribute('data-payment_proof');

//        // Update gambar
//        var img = basicModal.querySelector('#payment-proof-img');
//        img.src = '/storage/assets/payment_proof/' + paymentProof;

//        // Update action form
//        document.getElementById('reject-form').action = '/transactions/' + id + '/reject';
//        document.getElementById('accept-form').action = '/transactions/' + id + '/accept';
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    var pictureModal = document.getElementById('pictureModal');
//    pictureModal.addEventListener('show.bs.modal', function (event) {
//        var button = event.relatedTarget; // Tombol yang diklik
//        var id = button.getAttribute('data-id');
//        var requestPicture = button.getAttribute('data-request-picture');

//        // Update gambar
//        var img = pictureModal.querySelector('#request-picture-img');
//        img.src = '/storage/assets/request_picture/' + requestPicture;
//    });
//});

document.addEventListener('DOMContentLoaded', function () {
    // Preview profile image before upload
    document.getElementById('profileImage').addEventListener('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                document.getElementById('profilePreview').src = event.target.result;
            };
            reader.readAsDataURL(file);
        }
    });


    // Password strength indicator
    const newPassword = document.getElementById('newPassword');
    if (newPassword) {
        newPassword.addEventListener('input', function () {
            const strength = checkPasswordStrength(this.value);
            const progressBar = this.parentElement.querySelector('.progress-bar');
            const strengthText = this.parentElement.querySelector('.strength-text');

            progressBar.style.width = strength.percentage + '%';
            progressBar.className = 'progress-bar bg-' + strength.color;
            strengthText.textContent = strength.text;
            strengthText.className = 'strength-text text-' + strength.color;
        });
    }

    // Password confirmation check
    const renewPassword = document.getElementById('renewPassword');
    if (renewPassword) {
        renewPassword.addEventListener('input', function () {
            const matchDiv = document.getElementById('passwordMatch');
            if (this.value === newPassword.value) {
                matchDiv.innerHTML = '<i class="bi bi-check-circle-fill text-success me-1"></i>Password cocok';
            } else {
                matchDiv.innerHTML = '<i class="bi bi-exclamation-circle-fill text-danger me-1"></i>Password tidak cocok';
            }
        });
    }

    // Check password strength
    function checkPasswordStrength(password) {
        let strength = 0;

        // Check length
        if (password.length >= 8) strength++;
        if (password.length >= 12) strength++;

        // Check for mixed case
        if (/[a-z]/.test(password) && /[A-Z]/.test(password)) strength++;

        // Check for numbers
        if (/\d/.test(password)) strength++;

        // Check for special chars
        if (/[^a-zA-Z0-9]/.test(password)) strength++;

        // Determine strength level
        if (strength <= 2) {
            return { percentage: 33, color: 'danger', text: 'Lemah' };
        } else if (strength <= 4) {
            return { percentage: 66, color: 'warning', text: 'Sedang' };
        } else {
            return { percentage: 100, color: 'success', text: 'Kuat' };
        }
    }
});