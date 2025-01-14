using contracts.binding_models;
using contracts.worker_contracts.helper_models;

namespace contracts.worker_contracts {
    public interface Itemplate_worker {
        public byte[] create_template_file(template_binding_model model);
        public Idata_info read_temp_file(template_binding_model model);
    }
}
