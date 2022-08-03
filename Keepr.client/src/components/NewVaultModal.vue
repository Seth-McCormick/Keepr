<template>



    <!-- Modal -->
    <div class="modal fade" id="vault-modal" tabindex="-1" role="dialog" aria-labelledby="modelTitleId"
        aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="d-flex justify-content-between m-2">
                    <h2>New Vault</h2>
                    <button type="button" title="Close" class="btn-close btn-danger" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>



                <div class="modal-body">
                    <form @submit.prevent="createVault">
                        <h5 class="">Title</h5>
                        <input id="name" name="name" placeholder="Title..." class="form-control mb-3" type="text"
                            v-model="vaultData.name">
                        <h5 class="">Image Url</h5>
                        <input id="img" name="img" placeholder="Url..." class="form-control mb-3" type="text"
                            v-model="vaultData.img">

                        <div class="d-flex">
                            <input type="checkbox" id="isPrivate" v-model="vaultData.isPrivate">
                            <h5 class="m-0 ms-2">Private?</h5>
                        </div>
                        <p class="text-muted">Private Vaults can only be seen by you</p>
                        <div class="d-flex justify-content-end m-2">
                            <button type="submit" class="btn btn-success">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</template>


<script>
import { ref } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {


    setup() {
        const vaultData = ref({})
        return {
            vaultData,
            async createVault() {
                try {
                    await vaultsService.createVault(vaultData.value)
                    Pop.toast("Vault Created")
                } catch (error) {
                    Pop.toast(error, "error")
                    logger.log(error)
                }
            },

        }
    }
}
</script>


<style lang="scss" scoped>
</style>