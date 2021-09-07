import ResourceVI from "./resource";

class ValidateFn {
    /**
     * 
     * @param {*} input xâu input cần validate
     * @param {*} dataType kiểu dữ liệu cần validate
     * @returns 
     */
    validateInputFormat(input, dataType, isRequired) {
        switch (dataType) {
            case 'PersonName':
                return this.validName(input, isRequired);
            case 'Email':
                return this.validEmail(input, isRequired);
            default:
                if (!input && isRequired == true)
                    return ResourceVI.PopupMessage.NotNull;
                return 'Correct';
        }
    }

    /**
     * @param {} myname Xâu chứa tên cần validate
     * @returns Message Kết quả validate
     *  Nguyễn Hùng 18/07
     */
    validName(myname, isRequired) {
        if (!myname && isRequired) return ResourceVI.PopupMessage.NotNull;
        for (var i = 0; i < myname.length; i++) {
            if (!isNaN(parseInt(myname[i], 10))) {
                return ResourceVI.PopupMessage.ContainAlphabetsOnly;
            }
        }
        return 'Correct';
    }

    /**
     * @param {} myemail Xâu chứa email cần validate
     * @returns Message Kết quả validate
     *  Nguyễn Hùng 18/07
     */
    validEmail(myemail, isRequired) {
        if (!myemail && isRequired) return ResourceVI.PopupMessage.NotNull;
        else if (myemail) {
            const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (re.test(String(myemail).toLowerCase()) == true) return 'Correct';
            return ResourceVI.PopupMessage.InvalidFormat;
        } else {
            return "Correct";
        }
    }
}
export default new ValidateFn()