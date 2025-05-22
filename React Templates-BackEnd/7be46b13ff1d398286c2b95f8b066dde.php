<?php $__env->startPush('modals-container'); ?>
    <div class="modal fade center-scale"
         id="screen-modal-<?php echo e($key); ?>"
         role="dialog"
         aria-labelledby="screen-modal-<?php echo e($key); ?>"
         data-controller="modal"
         data-modal-slug="<?php echo e($templateSlug); ?>"
         data-modal-async-enable="<?php echo e($asyncEnable); ?>"
         data-modal-async-route="<?php echo e($asyncRoute); ?>"
         data-modal-open="<?php echo e($open); ?>"
        <?php echo e($staticBackdrop ? "data-bs-backdrop=static" : ''); ?>

    >
        <div class="modal-dialog modal-fullscreen-md-down <?php echo e($size); ?> <?php echo e($type); ?>"
             role="document"
             id="screen-modal-type-<?php echo e($key); ?>"
        >
            <form class="modal-content"
                  action="<?php echo e($method); ?>"
                  id="screen-modal-form-<?php echo e($key); ?>"
                  method="post"
                  enctype="multipart/form-data"
                  data-controller="form"
                  data-action="form#submit"
            >
                <div class="p-4 modal-placeholder">
                    <div class="placeholder-glow mb-3 mt-1 d-flex align-items-center">
                        <span class="placeholder col-6 h5 me-auto rounded-1"></span>
                        <button type="button" class="btn-close" title="Close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <p class="placeholder-glow">
                        <span class="placeholder col-7 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-6 rounded-1"></span>
                        <span class="placeholder col-8 rounded-1"></span>
                        <span class="placeholder col-7 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-6 rounded-1"></span>
                        <span class="placeholder col-8 rounded-1"></span>
                        <span class="placeholder col-3 rounded-1"></span>
                        <span class="placeholder col-7 rounded-1"></span>
                    </p>

                    <p class="placeholder-glow mb-0">
                        <span class="placeholder col-3 rounded-1"></span>
                        <span class="placeholder col-7 rounded-1"></span>
                        <span class="placeholder col-7 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                        <span class="placeholder col-6 rounded-1"></span>
                        <span class="placeholder col-8 rounded-1"></span>
                        <span class="placeholder col-4 rounded-1"></span>
                    </p>
                </div>
                <div class="modal-header">
                    <h4 class="modal-title text-black fw-light" data-modal-target="title"><?php echo e($title); ?></h4>
                    <button type="button" class="btn-close" title="Close" data-bs-dismiss="modal"
                            aria-label="Close">
                    </button>
                </div>
                <div class="modal-body layout-wrapper">
                    <?php if (isset($component)) { $__componentOriginala871b0937f833a73f8d6540e05f15b48 = $component; } ?>
<?php $component = Orchid\Platform\Components\Stream::resolve([] + (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag ? (array) $attributes->getIterator() : [])); ?>
<?php $component->withName('orchid-stream'); ?>
<?php if ($component->shouldRender()): ?>
<?php $__env->startComponent($component->resolveView(), $component->data()); ?>
<?php if (isset($attributes) && $attributes instanceof Illuminate\View\ComponentAttributeBag && $constructor = (new ReflectionClass(Orchid\Platform\Components\Stream::class))->getConstructor()): ?>
<?php $attributes = $attributes->except(collect($constructor->getParameters())->map->getName()->all()); ?>
<?php endif; ?>
<?php $component->withAttributes(['target' => ''.e($templateSlug).'']); ?>
                        <div id="<?php echo e($templateSlug); ?>">
                            <?php $__currentLoopData = $manyForms; $__env->addLoop($__currentLoopData); foreach($__currentLoopData as $formKey => $modal): $__env->incrementLoopIndices(); $loop = $__env->getLastLoop(); ?>
                                <?php $__currentLoopData = $modal; $__env->addLoop($__currentLoopData); foreach($__currentLoopData as $item): $__env->incrementLoopIndices(); $loop = $__env->getLastLoop(); ?>
                                    <?php echo $item ?? ''; ?>

                                <?php endforeach; $__env->popLoop(); $loop = $__env->getLastLoop(); ?>
                            <?php endforeach; $__env->popLoop(); $loop = $__env->getLastLoop(); ?>
                            <?php echo csrf_field(); ?>
                        </div>
                     <?php echo $__env->renderComponent(); ?>
<?php endif; ?>
<?php if (isset($__componentOriginala871b0937f833a73f8d6540e05f15b48)): ?>
<?php $component = $__componentOriginala871b0937f833a73f8d6540e05f15b48; ?>
<?php unset($__componentOriginala871b0937f833a73f8d6540e05f15b48); ?>
<?php endif; ?>
                </div>
                <div class="modal-footer">
                    <?php if(!$withoutCloseButton): ?>
                        <button type="button" class="btn btn-link" data-bs-dismiss="modal">
                            <?php echo e($close); ?>

                        </button>
                    <?php endif; ?>

                    <?php if(empty($commandBar)): ?>
                        <?php if(!$withoutApplyButton): ?>
                            <button type="submit"
                                    id="submit-modal-<?php echo e($key); ?>"
                                    data-turbo="<?php echo e(var_export($turbo)); ?>"
                                    class="btn btn-default">
                                <?php echo e($apply); ?>

                            </button>
                        <?php endif; ?>
                    <?php else: ?>
                        <?php echo $commandBar; ?>

                    <?php endif; ?>
                </div>
            </form>
        </div>
    </div>
<?php $__env->stopPush(); ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/orchid/platform/resources/views/layouts/modal.blade.php ENDPATH**/ ?>