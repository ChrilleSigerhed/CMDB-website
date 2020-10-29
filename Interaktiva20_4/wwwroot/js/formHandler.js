

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
let SavedList = localStorage.getItem("savedList")
let LikedOrDislikedAlready = SavedList.split(",");


document.querySelectorAll('.icon-thumbs-up').forEach(item => {
    item.addEventListener('click', event => {

        if (LikedOrDislikedAlready.includes(item.accessKey) == false) {
            var request = new XMLHttpRequest()
            request.open('GET', 'https://localhost:44313/api/' + item.accessKey + '/like', true)
            request.onload = function () {
                if (request.status == 200) {
                    let value = item.textContent
                    value++
                    item.textContent = value
                    LikedOrDislikedAlready.push(item.accessKey);
                    localStorage.setItem("savedList", LikedOrDislikedAlready);
                }
                else {
                    alert("Ops, något gick fel!")
                }
            }
            request.send()
        }
        else {
            alert("Du har redan röstat på den filmen!");
        }
    })
})
document.querySelectorAll('.icon-thumbs-down').forEach(item => {
    item.addEventListener('click', event => {

        if (LikedOrDislikedAlready.includes(item.accessKey) == false) {
            var request = new XMLHttpRequest()
            request.open('GET', 'https://localhost:44313/api/' + item.accessKey + '/dislike', true)
            request.onload = function () {
                if (request.status == 200) {
                    let value = item.textContent
                    value++
                    item.textContent = value
                    LikedOrDislikedAlready.push(item.accessKey)
                }
                else {
                    alert("Ops, något gick fel!")
                }
            }
            request.send()
        }
        else {
            alert("Du har redan röstat på den filmen!");
        }
    })
})