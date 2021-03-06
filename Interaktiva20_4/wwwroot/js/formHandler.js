﻿
document.getElementById('myUL').style.display = "none"
function onChange(val) {
    window.location="/about/search?selectedMovie=" +val
}

document.getElementById('myInput').addEventListener('keyup', function search(event) {
    if (event.keyCode == 13) {
        window.location = "/search?ID=" + this.value
    }
})



function searchFunction() {
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



let LikedOrDislikedAlready = new Array
if (sessionStorage.getItem('savedList') != null) {
    LikedOrDislikedAlready = JSON.parse(sessionStorage.getItem('savedList'))
}
function SaveVote() {
    sessionStorage.setItem('savedList', JSON.stringify(LikedOrDislikedAlready))
}
// Eventlistener för alla "Gilla knappar" som registerar en like
document.querySelectorAll('.icon-thumbs-up').forEach(item => {
    item.addEventListener('click', async function () {
        if (sessionStorage.getItem('savedList') != null)
        {
            let AlreadyVoted = JSON.parse(sessionStorage.getItem('savedList'))
            if (AlreadyVoted.includes(item.accessKey) == false)
            {
                let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/like')
                if (request.status == 200) {
                    let data = await request.json()
                    item.textContent = data.numberOfLikes
                    LikedOrDislikedAlready.push(item.accessKey)
                    SaveVote();
                }
                else {
                    alert("Ops, något gick fel!")
                }
            }
            else {
                alert("Du har redan röstat på den filmen!");
            }
        }
        else
        {
            let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/like')
            if (request.status == 200)
            {
                let data = await request.json()
                item.textContent = data.numberOfLikes
                LikedOrDislikedAlready.push(item.accessKey)
                SaveVote();
            }
            else
            {
            alert("Ops, något gick fel!")
            }
        }
    })
})

// Eventlistener för alla "Ogilla knappar" som registerar en dislike
document.querySelectorAll('.icon-thumbs-down').forEach(item => {
    item.addEventListener('click', async function() {
        if (sessionStorage.getItem('savedList') != null) {
            let AlreadyVoted = JSON.parse(sessionStorage.getItem('savedList'))
            if (AlreadyVoted.includes(item.accessKey) == false) {
                let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/dislike')
                if (request.status == 200) {
                    let data = await request.json()
                    item.textContent = data.numberOfDislikes
                    LikedOrDislikedAlready.push(item.accessKey)
                    SaveVote();
                }
                else {
                    alert("Ops, något gick fel!")
                }
            }
            else {
                alert("Du har redan röstat på den filmen!");
            }
        }
        else {
            let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/dislike')
            if (request.status == 200) {
                let data = await request.json()
                item.textContent = data.numberOfDislikes
                LikedOrDislikedAlready.push(item.accessKey)
                SaveVote();
            }
            else {
                alert("Ops, något gick fel!")
            }
        }
    })
})


let TopMoviePlot = document.querySelector('.movie_plot')
const fullPlot = TopMoviePlot.textContent
const button = document.createElement("a")
const shortPlotText = TopMoviePlot.textContent.substring(0, 150) + "..."
button.className = "readMoreButton"
TopMoviePlot.textContent = shortPlotText
if (fullPlot.length > 150) {
    button.textContent = "Show more"
    TopMoviePlot.appendChild(button)
}
button.addEventListener('click', function() {
    if (button.textContent == "Show more") {
        TopMoviePlot.textContent = fullPlot
        button.textContent = "Show less"
        TopMoviePlot.appendChild(button)
    }
    else {
        TopMoviePlot.textContent = shortPlotText
        button.textContent = "Show more"
        TopMoviePlot.appendChild(button)
    }
})

