function DeleteClass(url, userExist) {

    if (userExist) {

        Swal.fire({
            title: "Are you sure?",
            text: "Users Are Associated with this class",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {

            if (result.isConfirmed) {

                var xmlHttp = new XMLHttpRequest();


                xmlHttp.onreadystatechange = () => {

                    if (xmlHttp.readyState == 4 && (xmlHttp.status >= 200 && xmlHttp.status < 400)) {

                        var result = JSON.parse(xmlHttp.responseText);

                        if (result.success) {

                            toastr.success(result.message);
                            window.location.href = "/Admin/Admin/Classes";
                        }
                        else {
                            toastr.error(message);
                            window.location.href = "/Admin/Admin/Classes";
                        }

                    }

                }



                xmlHttp.open('DELETE', url, true);
                xmlHttp.send();

            }

        });

    }
    else {

        var xmlHttp = new XMLHttpRequest();


        xmlHttp.onreadystatechange = () => {

            if (xmlHttp.readyState == 4 && (xmlHttp.status >= 200 && xmlHttp.status < 400)) {

                var result = JSON.parse(xmlHttp.responseText);

                if (result.success) {

                    toastr.success(result.message);
                    window.location.href = "/Admin/Admin/Classes";
                }
                else {
                    toastr.error(message);
                    window.location.href = "/Admin/Admin/Classes";
                }

            }

        }

        xmlHttp.open('DELETE', url, true);
        xmlHttp.send();

    }


}