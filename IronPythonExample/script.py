def get_hello_world():
	return "Hello World!"

def do_magic(number):
	numbersList = []
	for i in range(0, 11):
		numbersList.append(i * number)

	return numbersList

def get_list_length(myList):
	return len(myList)

def get_amount_of_keys_in_object(myObject):
	return len(myObject)