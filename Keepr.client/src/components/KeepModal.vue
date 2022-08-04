<template>
    <Modal id="keep-modal">
        <template #modal-body>
            <div class="row">
                <div class="col-md-6">
                    <img class="rounded" :src="keep.img" alt="">
                </div>
                <div class="col-md-6 d-flex flex-column justify-content-around">
                    <div class="d-flex justify-content-end">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" title="Close"
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


                        <select name="vault" id="vault" @change="addToVault" v-model="vaultId">
                            <!-- v-for over the userVaults -->
                            <option value="0" disabled selected hidden>ADD TO VAULT</option>
                            <option v-for="v in vaults" :key="v.id" :value="v.id">{{ v.name }}</option>
                        </select>


                        <!-- <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1"
                                data-bs-toggle="dropdown" aria-expanded="false">
                                ADD TO VAULT
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                                <li><a class="dropdown-item" href="#" v-for="v in vaults" :key="v.id" :value="vaults.id"
                                        @click="addToVault(vaultKeepData)">{{ v.name }}</a></li>
                            </ul>
                        </div> -->

                        <div>
                            <i class="mdi mdi-delete selectable mdi-24px" title="Delete"
                                v-if="keep.creatorId == account.id" @click="deleteKeep"></i>
                        </div>

                        <div class="d-flex align-items-center" @click="goToProfile">
                            <img :src="keep.creator" alt="" height="40" class="rounded profile-img" />
                            <span class="mx-3 selectable lighten-30"><b>{{ keep.creator?.name }}</b></span>
                        </div>

                    </div>
                </div>


            </div>

        </template>
    </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {

    setup() {
        const router = useRouter()
        const vaultId = ref(0)
        onMounted(async () => {
            await profilesService.getUsersVaults()
        })
        return {
            vaultId,
            user: computed(() => AppState.user),
            account: computed(() => AppState.account),
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.usersVaults),
            activeKeep: computed(() => AppState.activeKeep),
            goToProfile() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
                    router.push({ name: "Profile", params: { id: this.activeKeep.creatorId } })
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },

            async addToVault() {
                const newVaultKeep = {
                    vaultId: vaultId.value,
                    keepId: this.keep.id
                }
                try {

                    await vaultKeepsService.addToVault(newVaultKeep)
                    vaultId.value = 0
                    Pop.toast("Added to Vault")
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },

            async deleteKeep() {
                try {
                    if (await Pop.confirm()) {
                        await keepsService.deleteKeep(this.keep.id)
                        Pop.toast("Keep Deleted")
                    }
                } catch (error) {
                    Pop.toast("Not Your Keep")
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

@media (max-width: 756px) {
    img {
        width: 100%;
        height: 100%;

    }
}
</style>