
class MainTree {
    constructor(selProc) {
        this.tree = null;
        this.onSelect = selProc;
        this.loading = true;
    }
    load(data, sel_id) {
        let that = this;

        this.loading = true;

        if (this.tree) {
            this.tree.removeData();
        }
        this.tree = $('#cont-tree').jstree({
            'core' : {
                'themes':{"dots":false, icons : false},
                'data' : data
            }
        }).on('changed.jstree', function (e, data) {
            if (!that.loading && that.onSelect && data.selected.length > 0) {
                that.onSelect(data.instance.get_node(data.selected[0]).id);
            }
        }).on('loaded.jstree', function() {
            if (sel_id) {
                $(this).jstree('select_node', sel_id);
                that.onSelect(sel_id);
            }
            $(this).jstree('open_all');
            setTimeout(() => {
                that.loading = false;
            }, 500);
        });
    }
}
