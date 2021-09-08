import EntityModel from "../model/entitymodel";
import DefaultConfig from "../defaultconfig.js";
import CommonFn from './commonfunction'

class FormatFn {
    formatTableData(entities, entityType) {
        let vm = this;
        let modelProperties = EntityModel[entityType];
        entities.forEach(entity => {
            for (let propName in modelProperties) {
                let propValue = entity[propName],
                    dataType = modelProperties[propName].dataType;
                if (dataType == "Enum") {
                    entity[propName + "Name"] = vm.formatViewData(propValue, dataType, propName);
                } else {
                    entity[propName] = vm.formatViewData(propValue, dataType, propName);
                }
            }
        });
        return entities;
    }


    formatViewData(data, dataType, propName) {
        let datax = data + '';
        switch (dataType) {
            case 'Date':
                return this.formatViewDate(datax)
            case 'Number':
                return this.formatMoney(datax)
            case 'Enum':
                return CommonFn.getEnumText(propName, data);
            default:
                return data;
        }
    }

    formatEntityData(entity, entityType) {
        let vm = this;
        let modelProperties = EntityModel[entityType];
        for (let propName in modelProperties) {
            let propValue = entity[propName],
                dataType = modelProperties[propName].dataType;
            entity[propName] = vm.formatOriginData(propValue, dataType, propName);
        }
        return entity;
    }

    formatOriginData(data, dataType) {
        let datax = data + '';
        switch (dataType) {
            case 'Date':
                return this.formatDateYMD(datax)
            default:
                return data;
        }
    }


    /**
     * @param {} thisdate Xâu javascript date
     * @returns Xâu ở dạng yyyy-MM-dd
     *  Nguyễn Hùng 18/07
     */
    formatDateYMD(thisdate) {
        thisdate += "";
        if (thisdate.length == 19 || thisdate.length == 10) {
            let dateArr = thisdate.split("T");
            if (dateArr[0].length == 10) {
                let date = new Date(dateArr[0]);
                return date.getFullYear() + '-' + ("0" + (date.getMonth() + 1)).slice(-2) + '-' + ("0" + date.getDate()).slice(-2);
            }
        }
        return null;
    }

    /**
     * @param {} thisdate Xâu javascript date
     * @returns Xâu ở dạng dd/MM/yyyy
     */
    formatViewDate(thisdate) {
        thisdate += "";
        if (thisdate.length == 19 || thisdate.length == 10) {
            let dateArr = thisdate.split("T");
            if (dateArr[0].length == 10) {
                let date = new Date(dateArr[0]);
                if (DefaultConfig.DateFormat == "DD/MM/YYYY")
                    return ("0" + date.getDate()).slice(-2) + '/' + ("0" + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear();
                else if (DefaultConfig.DateFormat == "MM/DD/YYYY") return ("0" + (date.getMonth() + 1)).slice(-2) + '/' + ("0" + (date.getDate())).slice(-2) + '/' + date.getFullYear();

            }
        }
        return "";

    }

    /**
     * 
     * @param {} myinput  xâu bất kỳ
     * @returns Xâu số (dấu ngăn . cách phần nghìn )
     */
    formatMoney(myinput) {
        myinput += "";
        if (myinput != null) {
            myinput.replaceAll(".", "");

            let onlynumber = '';
            for (var i = 0; i < myinput.length; i++) {
                if (!isNaN(parseInt(myinput[i], 10))) {
                    onlynumber += myinput[i];
                }
            }
            return (Number(onlynumber).toLocaleString('vi'));
        }
        return 0;
    }

    /**
     * @param {} myinput Xâu  input bất kỳ
     * @returns Xâu chỉ chứa số
     *  Nguyễn Hùng 18/07
     */
    formatNumber(myinput) {
        myinput += "";
        if (myinput != null) {
            myinput.replaceAll(".", "")

            let onlynumber = '';
            for (var i = 0; i < myinput.length; i++) {
                if (!isNaN(parseInt(myinput[i], 10))) {
                    onlynumber += myinput[i];
                }
            }
            return onlynumber;
        }
        return 0;
    }


    formatString(stringTemplate, ...strValues) {
        for (let i = 0; i < strValues.length; i++) {
            stringTemplate = stringTemplate.replace(`{${i}}`, strValues[i] + '');
        }
        stringTemplate = stringTemplate.replace(/{\d+}/, "")
        return stringTemplate;
    }

    removeAccents(str) {
        return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "").replace(/đ/g, 'd').replace(/Đ/g, 'D');
    }

    includeIgnoreCase(originStr, keywordStr) {
        if (!originStr) return false;
        if (!keywordStr) return false;
        originStr = originStr.toLowerCase();
        keywordStr = keywordStr.toLowerCase();
        return this.removeAccents(originStr).includes(this.removeAccents(keywordStr));
    }
}

export default new FormatFn();