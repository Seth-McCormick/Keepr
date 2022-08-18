<template>


    <!-- Modal -->
    <div class="modal fade" id="vault-keep-modal" tabindex="-1" role="dialog" aria-labelledby="modelTitleId"
        aria-hidden="true">
        <div class="modal-dialog  modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">

                <div class="row">
                    <div class="col-md-6">
                        <img class="rounded p-2" :src="keep.img" alt="">
                    </div>
                    <div class="col-md-6 d-flex flex-column justify-content-around">
                        <div class="d-flex justify-content-end">
                            <button type="button" class="btn-close m-2" data-bs-dismiss="modal"
                                aria-label="Close"></button>
                        </div>
                        <div class="d-flex  justify-content-center">
                            <h5 class="mx-3 mb-0">Views: {{ keep.views }}</h5>
                            <h5 class="mx-3 mb-0">Kepts: {{ keep.kept }}</h5>
                        </div>
                        <h1 class="text-center ">{{ keep.name }}</h1>
                        <p>{{ keep.description }}</p>
                        <p class="border-bottom border-dark"></p>
                        <div class="d-flex justify-content-between ">


                            <!-- 
                        <select name="vaults" id="vaults" @change="createVaultKeep">
                            <option v-for="v in vaults" :key="v.id" :value="vaults.id">{{ v.name }}</option>
                        </select> -->


                            <div>
                                <button v-if="keep.creator?.id == account.id" class="border-secondary rounded"
                                    @click="removeFromVault()">Remove
                                    From
                                    Vault</button>
                            </div>



                            <div class="d-flex align-items-center" @click="goToProfile">
                                <img :src="keep.creator?.img" alt="" height="40" class="rounded profile-img" />
                                <span class="mx-3 selectable lighten-30"><b>{{ keep.creator?.name }}</b></span>
                            </div>

                        </div>
                    </div>


                </div>


            </div>
        </div>
    </div>

</template>


<script>
import { Modal } from 'bootstrap'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'

import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    setup() {
        const router = useRouter()
        // onMounted(async () => {
        //     await vaultsService.getVaultKeeps()
        // })


        return {
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.usersVaults),
            // activeKeep: computed(() => AppState.activeKeep),
            vaultKeep: computed(() => AppState.vaultKeeps),
            account: computed(() => AppState.account),
            user: computed(() => AppState.user),
            goToProfile() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).hide()
                    router.push({ name: "Profile", params: { id: this.activeKeep.creatorId } })
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },

            async removeFromVault() {
                try {
                    if (await Pop.confirm()) {

                        Modal.getOrCreateInstance(document.getElementById('vault-keep-modal')).hide()
                        await vaultKeepsService.remove(this.keep.vaultKeepId)
                        Pop.toast('Keep Removed')
                    }


                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
img {
    width: 100%;
    height: 100%;
    object-fit: contain;
}

.profile-img {
    height: 10px;
    width: 10px;
    border-radius: 25%;
}
</style>