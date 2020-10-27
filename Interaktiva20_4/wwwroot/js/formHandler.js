

function onChange(val) {
    window.location="/about/search?selectedMovie=" +val
}

document.getElementById('myUL').style.display = 'none'
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
    let likeButton = document.getElementById("likeButton");
    likeButton.addEventListener("click", function () {
    let value = document.querySelector("#Rank1Movie").textContent
    value++
    document.querySelector("#Rank1Movie").textContent = value
    });
