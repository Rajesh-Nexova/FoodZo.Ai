<?php $__currentLoopData = $generate(); $__env->addLoop($__currentLoopData); foreach($__currentLoopData as $crumbs): $__env->incrementLoopIndices(); $loop = $__env->getLastLoop(); ?>
    <?php if($crumbs->url() && !$loop->last): ?>
        <li class="<?php echo e($class); ?>">
            <a href="<?php echo e($crumbs->url()); ?>"><?php echo e($crumbs->title()); ?></a>
        </li>
    <?php else: ?>
        <li class="<?php echo e($class); ?> <?php echo e($active); ?>"><?php echo e($crumbs->title()); ?></li>
    <?php endif; ?>
<?php endforeach; $__env->popLoop(); $loop = $__env->getLastLoop(); ?>
<?php /**PATH /Users/george/Dropbox/code/dine-hub/backend/vendor/tabuna/breadcrumbs/src/../views/breadcrumbs.blade.php ENDPATH**/ ?>