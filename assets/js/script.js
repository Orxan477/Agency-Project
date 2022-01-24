const navbar = document.getElementById("navbar");

function myFunction() {
    if (window.scrollY >= 0) {
    navbar.lastElementChild.classList.remove("MyNavbar")
    navbar.lastElementChild.classList.add("MyNavbarScroll")
    } else {
        navbar.lastElementChild.classList.remove("MyNavbarScroll")
        navbar.lastElementChild.classList.add("MyNavbar")
    }
  }