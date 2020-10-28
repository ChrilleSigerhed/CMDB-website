﻿
document.getElementById('myUL').style.display = "none"
function onChange(val) {
    window.location="/about/search?selectedMovie=" +val
}

function myFunction() {

    var input, filter, ul, li, a, i, txtValue
    input = document.getElementById('myInput')
    filter = input.value.toUpperCase()
    ul = document.getElementById("myUL")
    li = ul.getElementsByTagName('li')
    let counter = 0

    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0]
        txtValue = a.textContent
        if (txtValue.toUpperCase().indexOf(filter) > -1 && counter < 5)
        {
            counter++
            li[i].style.display = "block"
            
        }
        else {
            li[i].style.display = "none"
        }
    }
    if (input.value.length == 0 || input.value == " ") {
        document.getElementById('myUL').style.display = 'none'
    }
    else
    {
        document.getElementById('myUL').style.display = 'block'
    }
}

 
const LikeButton = document.querySelector("#body_margin > div > div > div > div.TopMovieInfo > a.icon-thumbs-up")
LikeButton.addEventListener("click", function () {
    let value = LikeButton.textContent
    value++
    LikeButton.textContent = value
});
