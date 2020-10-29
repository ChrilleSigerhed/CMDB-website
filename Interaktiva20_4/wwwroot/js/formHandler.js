
function onChange(val) {
    window.location="/about/search?selectedMovie=" +val
}

document.getElementById('myInput').addEventListener('keyup', function search(event) {
    if (event.keyCode == 13) {
        window.location = "/search?ID=" + this.value
    }
})



function myFunction() {
    document.getElementById('myUL').style.display = "none"
    var input, filter, ul, li, a, i, txtValue
    input = document.getElementById('myInput')
    filter = input.value.toUpperCase()
    ul = document.getElementById("myUL")
    li = ul.getElementsByTagName('li')
    let numberOfBlocks = 0
    for (var i = 0; i < li.length; i++) {
        li[i].style.display = "none"
    }
    for (var i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0]
        p = a.querySelector("#titleTag").firstChild.nodeValue.toUpperCase()
        txtValue = p
        for (var j = 0; j < filter.length; j++) {
            if (txtValue.charAt(j) != filter.charAt(j)) {
                li[i].style.display = "none"
                break
            }
            else if(j == filter.length -1 && numberOfBlocks < 5) {
                li[i].style.display = "block"
                numberOfBlocks++
            }
        }
    }
    if (numberOfBlocks < 5) {
        for (i = 0; i < li.length; i++) {
            a = li[i].getElementsByTagName("a")[0]
            p = a.querySelector("#titleTag").firstChild.nodeValue.toUpperCase()
            txtValue = p
            if (li[i].style.display == "none" && numberOfBlocks < 5) {

                if (txtValue.toUpperCase().indexOf(filter) > -1)
                {
                    li[i].style.display = "block"
                    numberOfBlocks++            
                }
                else{
                    li[i].style.display = "none"
                }
            }
        }
    }
    if (numberOfBlocks < 5) {
        for (i = 0; i < li.length; i++) {
            a = li[i].getElementsByTagName("a")[0]
            p = a.querySelector("#actorTag").firstChild.nodeValue.toUpperCase()
            txtValue = p
            if (li[i].style.display == "none" && numberOfBlocks < 5) {

                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "block"
                    numberOfBlocks++
                }
                else {
                    li[i].style.display = "none"
                }
            }
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
