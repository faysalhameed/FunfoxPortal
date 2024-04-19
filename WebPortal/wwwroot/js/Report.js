

let reportSelect = document.querySelector("#reportSelect");

reportSelect.addEventListener('change', function () {

    var selectedOption = reportSelect.options[reportSelect.selectedIndex].value;

    let xmlHttp = new XMLHttpRequest();


    xmlHttp.onreadystatechange = () => {

        if (xmlHttp.readyState == 4 && (xmlHttp.status >= 200 && xmlHttp.status < 400)) {

            var result = JSON.parse(xmlHttp.response);

            var showusers = document.getElementById("showUsers");
            var showcount = document.getElementById("showCount");
            showusers.innerHTML = "";
            showcount.innerHTML = "";

            if (result.users != "") {

                showcount.textContent = `User Count: ${result.count}`;

                for (var usr of result.users) {

                    var tr = document.createElement('tr');
                    var td = document.createElement('td');
                    td.textContent = `${usr.firstName} ${usr.lastName}`;

                    tr.append(td);
                    showusers.append(tr);

                }

            }

        }

    }


    xmlHttp.open('GET', `/Admin/Admin/ReportGet/${selectedOption}`, true);
    xmlHttp.send();


    console.log(selectedOption);

})