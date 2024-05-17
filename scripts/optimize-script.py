import re


INSERT_BUFFER = None
def create_buffer(table, pattern):
    global INSERT_BUFFER
    INSERT_BUFFER = {
        "table": table,
        "pattern": pattern,
        "entries": []
    }


def match_buffer(table, pattern):
    return table == INSERT_BUFFER["table"] == table and pattern == INSERT_BUFFER["pattern"]


def append_buffer(entry):
    INSERT_BUFFER["entries"].append(entry)


def finish_buffer(hout):
    global INSERT_BUFFER
    if INSERT_BUFFER == None:
        return
    table = INSERT_BUFFER['table']
    columns = INSERT_BUFFER['pattern']
    values = INSERT_BUFFER['entries']
    sql_values = ','.join(values)
    sql_insert = f"INSERT INTO {table} {columns} VALUES {sql_values};"
    hout.write(f"{sql_insert}\n")
    INSERT_BUFFER = None


PTN_CMD = "^INSERT INTO"
PTN_TABLE = "INTO .*? "
PTN_COLUMNS = "\\(.*\\) VALUES"
PTN_VALUES = "VALUES .*"
def handle_line(line, hout):
    match = re.match(PTN_CMD, line)
    if match != None:
        table = re.findall(PTN_TABLE, line)[0][len("INTO "):].strip()
        columns = re.findall(PTN_COLUMNS, line)[0][:-len(' VALUES')].strip()
        values = re.findall(PTN_VALUES, line)[0][len('VALUES '):].strip()[:-1]
        if INSERT_BUFFER != None and len(INSERT_BUFFER['entries']) >= 50:
            finish_buffer(hout)
        if INSERT_BUFFER == None:
            create_buffer(table, columns)
        if match_buffer(table, columns):
            append_buffer(values)
        else:
            finish_buffer(hout)
    else:
        finish_buffer(hout)
        hout.write(line)

    


with open("optimized-mtg.sql", "w") as hout:
    with open("postgres-mtg.sql") as hin:
        for line in hin.readlines():
            handle_line(line, hout)
