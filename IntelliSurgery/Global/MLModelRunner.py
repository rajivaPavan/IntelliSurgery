import pickle
import sys

#Using the data passed to the file
age  = int(sys.argv[1])  
gender = int(sys.argv[2])
asa = int(sys.argv[3])
bmi = float(sys.argv[4])
complication = int(sys.argv[5])
surgeryType = str(sys.argv[6])
diseases = str(sys.argv[7])
file_path = str(sys.argv[8])+"\\hipLRwC.pkl"

#split the csv in diseases to make the list object
diseasesList = diseases.split(",")


loaded_model = []
with open(file_path, 'rb') as f:
    loaded_model = pickle.load(f)

#Since the diseases related to the patient are passed, initial values are set to zero.
#If the patient has a disease then the value will be changed to 1.
cancer = 0
cardiovascular = 0
dementia = 0
diabetes = 0
digestive = 0
osteoarthritis = 0
pyschologicalDisorder = 0
pulmonary = 0

if (len(diseasesList) != 0):
    for item in diseasesList:
        if item == "Cancer":
            cancer = 1
        elif item == "Cardiovascular":
            cardiovascular = 1
        elif item == "Dementia":
            dementia =1
        elif item == "Diabetes":
            diabetes = 1
        elif item == "Digestive":
            digestive = 1
        elif item == "Osteoarthritis":
            osteoarthritis = 1
        elif item == "PyschologicalDisorder":
            pyschologicalDisorder = 1
        elif item == "Pulmonary":
            pulmonary = 1
    

machineLearningModelInput = [[age,asa,gender,bmi,cancer,cardiovascular,dementia,diabetes,digestive,osteoarthritis,pyschologicalDisorder,pulmonary]]

def predictTheTime(modelInput):
    "This function is used to predict the time for the given surgery"
    predictedOutput = loaded_model.predict(modelInput)
    #print(modelInput, predictedOutput)
    return predictedOutput[0][0];

print(predictTheTime(machineLearningModelInput))


