(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('myRecipesController', MyRecipesController);

    MyRecipesController.$inject = ['$scope', '$baseController', '$uibModal', '$recipesService'];

    function MyRecipesController(
        $scope
        , $baseController
        , $uibModal
        , $recipesService) {

        var vm = this;
        vm.myRecipes = null;
        vm.singleRecipe = null;
        vm.mediaId = 0;

        vm.addNewRecipeContainer = false;
        vm.updateRecipeContainer = false;        
        vm.viewSingleRecipeContainer = false;
        vm.showRecipeError = false;

        vm.ingredientEditModeDisabled = true;

        vm.addIngredientFormField = [];
        vm.submitRecipeForm = null;
        vm.updateRecipeForm = null;
        vm.newRecipe = {};
        vm.updatedRecipe = {};
        vm.newRecipe.ingredients = [{ name: "", quantity: 0, measurementtype: 0 }];
        vm.updatedIngredient= [];

        vm.measurementtype = [
            { type: null, value: 0 },
            { type: 'tsp', value: 1 },
            { type: 'tbsp', value: 2 },
            { type: 'fl oz', value: 3 },
            { type: 'cup', value: 4 },
            { type: 'pint', value: 5 },
            { type: 'quart', value: 6 },
            { type: 'gallon', value: 7 },
            { type: 'pound', value: 8 },
            { type: 'ounce', value: 9 },
            { type: 'mg', value: 10 },
            { type: 'g', value: 11 },
            { type: 'kg', value: 12 },
        ];

        $baseController.merge(vm, $baseController);

        vm.$scope = $scope;
        vm.$uibModal = $uibModal;
        vm.$recipesService = $recipesService;

        vm.notify = vm.$recipesService.getNotifier($scope);

        vm.renderCurrentReviews = _renderCurrentReviews;
        vm.renderCurrentReviewsError = _renderCurrentReviewsError;

        vm.addIngredient = _addIngredient;
        vm.removeIngredient = _removeIngredient;

        vm.submitNewRecipe = _submitNewRecipe;
        vm.submitNewRecipeSuccess = _submitNewRecipeSuccess;
        vm.submitNewRecipeError = _submitNewRecipeError;

        vm.viewSingleRecipe = _viewSingleRecipe;
        vm.viewSingleRecipeSuccess = _viewSingleRecipeSuccess;
        vm.viewSingleRecipeError = _viewSingleRecipeError;

        vm.addNewRecipeButton = _addNewRecipeButton;
        vm.defaultMyRecipesView = _defaultMyRecipesView;

        vm.deleteRecipe = _deleteRecipe;
        vm.deleteRecipeSuccess = _deleteRecipeSuccess;
        vm.deleteRecipeError = _deleteRecipeError;

        vm.editRecipe = _editRecipe;

        vm.submitUpdatedRecipe = _submitUpdatedRecipe;
        vm.submitUpdatedRecipeSuccess = _submitUpdatedRecipeSuccess;
        vm.submitUpdatedRecipeError = _submitUpdatedRecipeError;

        vm.deleteIngredient = _deleteIngredient;
        vm.deleteIngredientSuccess = _deleteIngredientSuccess;
        vm.delteIngredientError = _deleteIngredientError;

        vm.updateIngredient = _updateIngredient;
        vm.updateIngredientSuccess = _updateIngredientSuccess;
        vm.updateIngredientError = _updateIngredientError;

        vm.addNewIngredientForRecipe = _addNewIngredientForRecipe;
        vm.removeNewIngredientForRecipe = _removeNewIngredientForRecipe;

        vm.openPictureModal = _openPictureModal;

        render();

        function render() {

            //load initial reviews
            vm.$recipesService.getCurrentRecipes(vm.renderCurrentReviews, vm.renderCurrentReviewsError);
        }

        function _renderCurrentReviews(data) {

            if (data !== null) {
                vm.notify(function () {
                    vm.myRecipes = data.items;
                });
            }
        }

        function _renderCurrentReviewsError(data) {
            console.log('error loading recipes');
        }

        function _addIngredient() {
            vm.newRecipe.ingredients.push({ name: "", quantity: 0, measurementtype: 0 });
        }

        function _removeIngredient(index) {
            vm.newRecipe.ingredients.splice(index, 1);
        }

        function _submitNewRecipe() {

            vm.showRecipeError = true;

            if (vm.submitRecipeForm.$valid) {
                vm.$recipesService.insert(vm.newRecipe, vm.submitNewRecipeSuccess, vm.submitNewRecipeError);
            } else {
                console.log('recipe form not valid');
            }
        }

        function _submitNewRecipeSuccess(data) {
            vm.$alertService.success("Your recipe was successfully added!");
            // hide add recipe container
            vm.addNewRecipeContainer = false;

            render();
        }

        function _submitNewRecipeError(error) {
            console.log('error submitting recipe', error);
        }

        function _viewSingleRecipe(id) {

            vm.viewSingleRecipeContainer = true;

            if (id !== null) {
                vm.$recipesService.getRecipeByRecipeId(id, vm.viewSingleRecipeSuccess, vm.viewSingleRecipeError);
            }
        }

        //render single recipe data
        function _viewSingleRecipeSuccess(data) {

            if (data !== null) {

                for (var i = 0; i < data.item.ingredients.length; i++) {

                    for (var j = 0; j < vm.measurementtype.length; j++) {
                        if (data.item.ingredients[i].measurementType == vm.measurementtype[j].value) {
                            data.item.ingredients[i].measurementType = vm.measurementtype[j].type;
                        }
                    }
                }

                vm.notify(function () {
                    vm.singleRecipe = data.item;

                    if (vm.singleRecipe.ingredients) {
                        for (var i = 0; i < vm.singleRecipe.ingredients.length; i++) {
                            vm.updatedIngredient.push(vm.singleRecipe.ingredients[i]);
                        }
                    }
                });
            }

            console.log(vm.updatedIngredient);
        }

        function _viewSingleRecipeError(error) {
            console.log('single recipe data did not load', error);
        }

        function _addNewRecipeButton() {
            vm.addNewRecipeContainer = true;
            vm.viewSingleRecipeContainer = false;
            vm.updateRecipeContainer = false;
        }

        function _defaultMyRecipesView() {
            vm.addNewRecipeContainer = false;
            vm.viewSingleRecipeContainer = false
        }

        function _deleteRecipe(recipeid) {

            if (confirm('Are you sure you want to delete this recipe?')) {
                vm.$recipesService.delete(recipeid, vm.deleteRecipeSuccess, vm.deleteRecipeError);
            }
        }

        function _deleteRecipeSuccess() {
            vm.$alertService.success("Your recipe was successfully deleted!");
            vm.addNewRecipeContainer = false;
            render();
        }

        function _deleteRecipeError() {
            console.log('error deleting recipe');
        }

        function _editRecipe() {
            vm.updateRecipeContainer = true;
            vm.viewSingleRecipeContainer = false;
            vm.addNewRecipeContainer = false;

            $("#updateRecipeForm").trigger("reset");

            vm.updatedRecipe = {
                id: vm.singleRecipe.id,
                name: vm.singleRecipe.name,
                description: vm.singleRecipe.description,
                directions: vm.singleRecipe.directions,
                preptime: vm.singleRecipe.preptime,
                totaltime: vm.singleRecipe.totaltime,
                numberofservings: vm.singleRecipe.numberofservings,
                ingredients: []
            };
        }

        function _submitUpdatedRecipe(recipeid) {
            if (recipeid !== null) {

                vm.updatedRecipe.MediaId = vm.mediaId;
                vm.$recipesService.updateRecipe(recipeid, vm.updatedRecipe, vm.submitUpdatedRecipeSuccess, vm.submitUpdatedRecipeError);
            }
        }

        function _submitUpdatedRecipeSuccess() {

            vm.updatedRecipe = {
                id: vm.singleRecipe.id,
                name: vm.singleRecipe.name,
                description: vm.singleRecipe.description,
                directions: vm.singleRecipe.directions,
                preptime: vm.singleRecipe.preptime,
                totaltime: vm.singleRecipe.totaltime,
                numberofservings: vm.singleRecipe.numberofservings,
                ingredients: []
            };

            render();

            vm.$alertService.success("Your recipe was successfully updated!");
            vm.addNewRecipeContainer = false;
            vm.updateRecipeContainer = false;
            vm.viewSingleRecipeContainer = false;
        }

        function _submitUpdatedRecipeError(jqXHR, textStatus, errorThrown) {
            console.log('error updating recipe', textStatus, errorThrown);
        }

        function _deleteIngredient(ingredientid, index) {

            if (confirm('Are you sure you want to delete this ingredient?')) {
                vm.$recipesService.deleteIngredient(ingredientid, vm.deleteIngredientSuccess(index), vm.deleteIngredientError);
            }
        }

        function _deleteIngredientSuccess(index) {
            vm.singleRecipe.ingredients.splice(index, 1);

            vm.$alertService.success("Your ingredient was successfully deleted!");
        }

        function _deleteIngredientError() {
            console.log('error deleting ingredient');
        }

        function _updateIngredient(ingredientid, index) {

            if (confirm('Press enter or ok to update this ingredient')) {
                vm.$recipesService.updateIngredient(ingredientid, vm.updatedIngredient[index], vm.updateIngredientSuccess, vm.updateIngredientError);
            }
            
        }

        function _updateIngredientSuccess() {

            //add toaster for ingredient successfully updated
            vm.$alertService.success("Your ingredient was successfully updated!");
        }

        function _updateIngredientError() {
            console.log('ingredient update error');
        }

        function _addNewIngredientForRecipe() {
            vm.updatedRecipe.ingredients.push({ name: "", quantity: 0, measurementtype: 0 });
        }

        function _removeNewIngredientForRecipe(index) {
            vm.updatedRecipe.ingredients.splice(index, 1);
        }

        function _openPictureModal() {
            var modalInstance = vm.$uibModal.open({
                animation: true,
                templateUrl: '/Scripts/sabio/yori/portal/templates/addMediaModal.html',
                controller: 'modalController as mc'
            });

            // When modal closes it re-renders the reviews onto page
            modalInstance.result.then(function (selectedItem) {
                vm.mediaId = selectedItem;
            }, function () {
                console.log('modal dismissed');
            });
        }
    }
})();
