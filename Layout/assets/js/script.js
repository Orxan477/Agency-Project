const navbar = document.getElementById("navbar");

function myFunction() {
    if (window.scrollY > 0) {
    navbar.lastElementChild.classList.remove("MyNavbar")
    navbar.lastElementChild.classList.add("MyNavbarScroll")
    navbar.parentElement.classList.add("navbarcolor")
    navbar.lastElementChild.lastElementChild.classList.add("navbarHeight")
    } else {
        navbar.lastElementChild.classList.remove("MyNavbarScroll")
        navbar.lastElementChild.classList.add("MyNavbar")
        navbar.parentElement.classList.remove("navbarcolor")
        navbar.lastElementChild.lastElementChild.classList.remove("navbarHeight")
    }
  }
  console.log(navbar.lastElementChild.lastElementChild)