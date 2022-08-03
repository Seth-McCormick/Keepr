<template>


    <!-- Modal -->
    <div class="modal fade" id="new-keep-modal" tabindex="-1" role="dialog" aria-labelledby="modelTitleId"
        aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="d-flex justify-content-between m-3">
                    <h1>New Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body">
                    <form @submit.prevent="createKeep">

                        <h5 class="m-0">Title</h5>
                        <input class="form-control mt-1 mb-3" placeholder="Title..." type="text"
                            v-model="keepData.name">
                        <h5 class="m-0">Image Url</h5>
                        <input class="form-control mt-1 mb-3" placeholder="URL..." type="text" v-model="keepData.img">
                        <h5 class="">Description</h5>
                        <textarea class="form-control" placeholder="Description..." name="" id="" cols="30" rows="5"
                            v-model="keepData.description"></textarea>
                        <div class="text-end mt-3">

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
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    setup() {
        const keepData = ref({})
        return {
            keepData,
            async createKeep() {
                try {
                    await keepsService.createKeep(keepData.value)
                    Pop.toast("Keep Created")
                } catch (error) {
                    Pop.toast(error, "error")
                    logger.log(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
</style>