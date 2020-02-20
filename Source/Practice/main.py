with open("a_example.in", "r") as information:
    data = list(information.read().splitlines())

# slices = data[1]

# first_line = data[0].split(" ")
# second_line = data[1].split(" ")

# max_slices = first_line[0]
# different_pizzas = first_line[1]

# slices_per_pizza = [none] * second_line

max_slices = data[0].split(" ")[0]
types_of_pizza = data[0].split(" ")[1]

n_slices_per_type = data[1].split(" ")



def test(n_type_of_pizza):
    i = 0
    for index, n_type in enumerate(n_slices_per_type):

        print(index, n_type)
        pass


# for n_slices in second_line:
    
#     pass

# print(n_slices_per_type)
