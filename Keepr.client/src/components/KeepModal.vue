<template>
    <Modal id="keep-modal">
        <template #modal-body>
            <div class="row">
                <div class="col-md-6">
                    <img class="rounded" :src="keep.img" alt="">
                </div>
                <div class="col-md-6 d-flex flex-column justify-content-around">
                    <div class="d-flex justify-content-end">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
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


                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1"
                                data-bs-toggle="dropdown" aria-expanded="false">
                                ADD TO VAULT
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                                <li><a class="dropdown-item" href="#" v-for="v in vaults" :key="v.id"
                                        :value="vaults.id">{{ v.name }}</a></li>
                            </ul>
                        </div>

                        <i class="mdi mdi-delete selectable d-flex align-items-center" title="Delete"></i>
                        <div class="d-flex align-items-center" @click="goToProfile">
                            <img :src="keep.creator?.img" alt="" height="40" class="rounded profile-img" />
                            <span class="mx-3 selectable lighten-30"><b>{{ keep.creator?.name }}</b></span>
                        </div>

                    </div>
                </div>


            </div>

        </template>
    </Modal>
</template>


<script>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    setup() {
        const router = useRouter()
        return {
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.usersVaults),
            activeKeep: computed(() => AppState.activeKeep),
            goToProfile() {
                try {
                    router.push({ name: "Profile", params: { id: this.activeKeep.creatorId } })
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },
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