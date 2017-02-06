(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('allRecipesController', AllRecipesController);

    AllRecipesController.$inject = ['$scope', '$baseController', '$recipesService'];

    function AllRecipesController(
        $scope
        , $baseController
        , $recipesService) {

        var vm = this;
        vm.allRecipes = null;
        vm.singleRecipe = null;
        vm.viewSingleRecipeContainer = false
        vm.updatedIngredient = [];

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
        vm.$recipesService = $recipesService;

        vm.notify = vm.$recipesService.getNotifier($scope);

        vm.getAllRecipesSuccess = _getAllRecipesSuccess;
        vm.getAllRecipesError = _getAllRecipesError;

        vm.viewSingleRecipe = _viewSingleRecipe;
        vm.viewSingleRecipeSuccess = _viewSingleRecipeSuccess;
        vm.viewSingleRecipeError = _viewSingleRecipeError;

        render();

        function render() {

            vm.$recipesService.getAllRecipes(vm.getAllRecipesSuccess, vm.getAllRecipesError);
        }

        function _getAllRecipesSuccess(data) {

            if (data !== null) {
                vm.notify(function () {
                    vm.allRecipes = data.items;
                });
            }
        }

        function _getAllRecipesError() {
            console.log('error loading all recipes');
        }

        function _viewSingleRecipe(id) {
            vm.viewSingleRecipeContainer = true;

            if (id !== null) {
                vm.$recipesService.getRecipeByRecipeId(id, vm.viewSingleRecipeSuccess, vm.viewSingleRecipeError);
            }
        }

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
        }

        function _viewSingleRecipeError(error) {

            console.log('single recipe data did not load', error);
        }

    }
})();