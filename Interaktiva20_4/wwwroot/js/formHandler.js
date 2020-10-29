

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

// Eventlistener för alla "Gilla knappar" som registerar en like
document.querySelectorAll('.icon-thumbs-up').forEach(item => {
    item.addEventListener('click', async function() {

        if (LikedOrDislikedAlready.includes(item.accessKey) == false) {
            let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/like')

            if (request.status == 200) {
                let value = item.textContent
                value++
                item.textContent = value
                LikedOrDislikedAlready.push(item.accessKey)
                localStorage.setItem('savedList', item.accessKey)
            }
            else {
                alert("Ops, något gick fel!")
            }


        }
        else {
            alert("Du har redan röstat på den filmen!");
        }
    })
})

// Eventlistener för alla "Ogilla knappar" som registerar en dislike
document.querySelectorAll('.icon-thumbs-down').forEach(item => {
    item.addEventListener('click', async function() {

        if (LikedOrDislikedAlready.includes(item.accessKey) == false) {
            let request = await fetch('https://localhost:44313/api/' + item.accessKey + '/dislike')
            
                if (request.status == 200) {
                    let value = item.textContent
                    value++
                    item.textContent = value
                    LikedOrDislikedAlready.push(item.accessKey)
                    localStorage.setItem('savedList', item.accessKey)
                }
                else {
                    alert("Ops, något gick fel!")
                }
           
            
        }
        else {
            alert("Du har redan röstat på den filmen!");
        }
    })
})

// Read more knappen för plot på den högst rankade filmen
TopMoviePlot = document.querySelector('#body_margin > div > div > div > div.TopMovieInfo > p')
const fullPlot = TopMoviePlot.textContent
const shortPlotText = TopMoviePlot.textContent.split(".")[0] + "..."
TopMoviePlot.textContent = shortPlotText
const button = document.createElement("a")
button.textContent = "Read more"
button.className = "readMoreButton"
TopMoviePlot.appendChild(button)
// Visar hela plotten
document.querySelector(".readMoreButton").addEventListener('click', function () {
    TopMoviePlot.textContent = fullPlot
})

