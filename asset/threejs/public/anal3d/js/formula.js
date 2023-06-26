
const formula = {
    push: ( key, proc ) => {
        this.formulas[key] = proc;
    },
    calc: ( key, args ) => {
        return this.formulas[key](args);
    },
    init: () => {

        this.formulas = {};
    
        formulaJS.forEach((el) => {
            var script = document.createElement("script");
            script.setAttribute("type", "text/javascript");
            script.setAttribute("src", "js/formulas/" + el + ".js");
            document.getElementsByTagName("head")[0].appendChild(script);
        });
    }
};
